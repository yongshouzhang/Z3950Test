using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Linq;

namespace Z3950.Marc
{
    internal enum RecordCharacterEncoding : byte
    {
        /// <summary> Marc Character encoding </summary>
        Marc = 1,

        /// <summary> Unicode character encoding </summary>
        Unicode,

        /// <summary> Unrecognized character encoding value found  (treated as Unicode) </summary>
        Unrecognized
    }
    public class MarcRecord
    {
        private string _controlNumber;
        private readonly SortedList<int, List<MarcField>> _fields;
        private string _leader;

        private MarcRecord()
        {
            _leader = string.Empty;
            _fields = new SortedList<int, List<MarcField>>();
            ErrorFlag = false;
        }
        public MarcRecord(Stream stream):this()
        {
            ErrorFlag = Parser(stream);
        }

        public MarcRecord(byte[] bytes):this()
        {
            using(MemoryStream ms= new MemoryStream(bytes))
            {
                ErrorFlag = Parser(ms);
            }
        }

        #region Public properties

        /// <summary> Control number for this record from the 001 field </summary>
        /// <remarks> This is used when importing directly from MARC records into the SobekCM library </remarks>
        public string ControlNumber
        {
            get
            {
                if (_controlNumber != null)
                    return _controlNumber;

                _controlNumber = _fields.ContainsKey(1) ? _fields[1][0].ControlFieldValue : String.Empty;

                return _controlNumber;
            }
        }

       
        public bool ErrorFlag { get; }

        /// <summary> Gets or sets the leader portion of this MARC21 Record </summary>
        public string Leader
        {
            get
            {
                // First, compute the overall length of this record
                var totalLength = 0;
                var directoryLength = 25;
                var allTags = SortedMarcTagList;
                foreach (MarcField thisTag in allTags)
                {
                    totalLength = totalLength + 5 + thisTag.ControlFieldValue.Length;
                    directoryLength += 12;
                }

                var totalLengthString = (totalLength.ToString()).PadLeft(5, '0');
                var totalDirectoryString = (directoryLength.ToString()).PadLeft(5, '0');

                if (_leader.Length == 0)
                {
                    return totalLengthString + "nam  22" + totalDirectoryString + "3a 4500";
                }
                return totalLengthString + _leader.Substring(5, 7) + totalDirectoryString + _leader.Substring(17);
            }
            set { _leader = value; }
        }

        /// <summary> Gets the collection of MARC fields, by MARC tag number </summary>
        /// <param name="tag"> MARC tag number to return all matching fields </param>
        /// <returns> Collection of matching tags, or an empty read only collection </returns>
        public ReadOnlyCollection<MarcField> this[int tag]
        {
            get
            {
                if (_fields.ContainsKey(tag))
                    return new ReadOnlyCollection<MarcField>(_fields[tag]);
                return new ReadOnlyCollection<MarcField>(new List<MarcField>());
            }
        }

        
        public List<MarcField> SortedMarcTagList
        {
            get
            {
                List<MarcField> returnValue = new List<MarcField>();

                foreach (List<MarcField> fieldsByTag in _fields.Values)
                {
                    returnValue.AddRange(fieldsByTag);
                }

                return returnValue;
            }
        }

        #endregion

        #region Public methods to check if a field exists, add a field, etc...
       
        private MarcField AddField(int tag, string controlFieldValue)
        {
            // Create the new control field
            var newField = new MarcField(tag, controlFieldValue);

            // Either add this to the existing list, or create a new one
            if (_fields.ContainsKey(tag))
                _fields[tag].Add(newField);
            else
            {
                var newTagCollection = new List<MarcField> {newField};
                _fields[tag] = newTagCollection;
            }

            // Return the newlly built control field
            return newField;
        }
       
        private MarcField AddField(int tag, char indicator1, char indicator2)
        {
            // Create the new datafield
            var newField = new MarcField(tag, indicator1, indicator2);

            // Either add this to the existing list, or create a new one
            if (_fields.ContainsKey(tag))
                _fields[tag].Add(newField);
            else
            {
                var newTagCollection = new List<MarcField> {newField};
                _fields[tag] = newTagCollection;
            }

            // Return the newlly built data field
            return newField;
        }
      
        private MarcField AddField(int tag, string indicators, string controlFieldValue)
        {
            // Create the new datafield
            var newField = new MarcField(tag, controlFieldValue) {Indicators = indicators};

            // Either add this to the existing list, or create a new one
            if (_fields.ContainsKey(tag))
                _fields[tag].Add(newField);
            else
            {
                var newTagCollection = new List<MarcField> {newField};
                _fields[tag] = newTagCollection;
            }

            // Return the newlly built data field
            return newField;
        }

        /// <summary> Adds a new field to this record </summary>
        /// <param name="newField"> New field to add </param>
        private void AddField(MarcField newField)
        {
            if (newField == null)
                return;

            // Either add this to the existing list, or create a new one
            if (_fields.ContainsKey(newField.Tag))
                _fields[newField.Tag].Add(newField);
            else
            {
                var newTagCollection = new List<MarcField> {newField};
                _fields[newField.Tag] = newTagCollection;
            }
        }
     
        public string Get_Data_Subfield(int tag, char subfield)
        {
            if ((_fields.ContainsKey(tag)) && (_fields[tag][0].has_Subfield(subfield)))
                return _fields[tag][0][subfield];
            return string.Empty;
        }

        #endregion

        #region Method overrides the ToString() method 

        /// <summary> Outputs this record as a string </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Create the StringBuilder
            StringBuilder returnVal = new StringBuilder(2000);

            // Add the leader
            returnVal.Append("LDR " + Leader + "\r\n");

            // Step through each field in the collection
            foreach (int thisTag in _fields.Keys)
            {
                var matchingFields = _fields[thisTag];
                foreach (var thisField in matchingFields)
                {
                    if (thisField.SubfieldCount == 0)
                    {
                        if (thisField.ControlFieldValue.Length > 0)
                        {
                            returnVal.Append(thisField.Tag.ToString().PadLeft(3, '0') + " " +
                                             thisField.ControlFieldValue + "\r\n");
                        }
                    }
                    else
                    {
                        returnVal.Append(thisField.Tag.ToString().PadLeft(3, '0') + " " + thisField.Indicators);

                        // Build the complete line
                        foreach (var thisSubfield in thisField.Subfields)
                        {
                            if (thisSubfield.SubfieldCode == ' ')
                            {
                                returnVal.Append(" " + thisSubfield.Data);
                            }
                            else
                            {
                                returnVal.Append(" |" + thisSubfield.SubfieldCode + " " + thisSubfield.Data);
                            }
                        }

                        returnVal.Append("\r\n");
                    }
                }
            }

            // Return the built string
            return returnVal.ToString();
        }

        #endregion

        #region Parser

        private bool Parser(Stream stream)
        {
            using (BinaryReader _reader = new BinaryReader(stream))
            {
                var thisRecord = this;

                var fieldDatas = new Dictionary<short, ParserVariableFieldData>();
                const char EndOfRecord = (char)29,
                    RecordSeperator = (char)30,
                    UnitSeperator = (char)31;

                try
                {
                    // Some values to check the end of the file
                    long fileLength = _reader.BaseStream.Length;

                    // Create the StringBuilder object for this record
                    var leaderBuilder = new StringBuilder(30);

                    // Read to first character
                    int result = _reader.Read();
                    bool eof = false;

                    // Read the leader and directory directly into a string, since this will not have specially
                    // coded characters ( leader and directory end with a RECORD_SEPERATOR )
                    int count = 0;
                    while ((!eof) && (result != EndOfRecord) && (result != RecordSeperator) && (count < 24))
                    {
                        // Want to skip any special characters at the beginning (like encoding characters)
                        if (result < 127)
                        {
                            // Save this character directly
                            leaderBuilder.Append((char)result);
                            count++;
                        }

                        // Read the next character and increment the count
                        if (_reader.BaseStream.Position < fileLength)
                        {
                            result = _reader.ReadByte();
                        }
                        else
                        {
                            eof = true;
                        }
                    }

                    // If this is the empty string, then just return null (DONE!)
                    if (eof)
                    {
                        return false;
                    }

                    // Ensure the leader was correctly retrieved
                    if (leaderBuilder.Length < 24)
                    {
                        throw new ApplicationException(
                            "Error reading leader.  Either end of file, group seperator, or record seperator found prematurely.");
                    }

                    // Save the leader into the record 
                    thisRecord.Leader = leaderBuilder.ToString();

                    // Verify the type of character encoding used here
                    RecordCharacterEncoding encoding = RecordCharacterEncoding.Unrecognized;
                    switch (thisRecord.Leader[9])
                    {
                        case ' ':
                            encoding = RecordCharacterEncoding.Marc;
                            break;

                        case 'a':
                            encoding = RecordCharacterEncoding.Unicode;
                            break;
                    }

                    // Now, read in all the directory information
                    var directoryEntries = new List<ParserDirectoryEntry>();
                    count = 0;
                    int tag = 0;
                    int fieldLength = 0;
                    int startingPosition = 0;
                    while ((result != EndOfRecord) && (result != RecordSeperator))
                    {
                        // Set the temp value to zero here
                        short temp = 0;
                        if (!short.TryParse(((char)result).ToString(), out temp))
                        {
                            throw new ApplicationException("Found invalid (non-numeric) character in a directory entry.");
                        }

                        // Increment different values, depending on how far into this directory
                        // the reader has gotten.
                        switch (count)
                        {
                            case 0:
                            case 1:
                            case 2:
                                tag = (tag * 10) + temp;
                                break;

                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                fieldLength = (fieldLength * 10) + temp;
                                break;

                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                startingPosition = (startingPosition * 10) + temp;
                                break;
                        }

                        // Read the next character
                        result = _reader.Read();
                        count++;

                        // If this directory entry has been completely read, save it
                        // and reset the values for the next directory
                        if (count == 12)
                        {
                            directoryEntries.Add(new ParserDirectoryEntry((short)tag, (short)fieldLength,
                                (short)startingPosition));
                            tag = 0;
                            fieldLength = 0;
                            startingPosition = 0;
                            count = 0;
                        }
                    }

                    // Use a memory stream to accumulate bytes (we don't yet know the character
                    // encoding for this record, so needs to remain bytes )
                    var byteFieldBuilder = new MemoryStream();

                    // Read all the data from the variable fields
                    count = 0;
                    var startIndex = 0;
                    short lastFieldStartIndex = 0;
                    result = _reader.Read();
                    while (result != EndOfRecord)
                    {
                        // Was this the end of the field (or tag)?
                        if (result == RecordSeperator)
                        {
                            // Get the value for this field
                            byte[] fieldAsByteArray = byteFieldBuilder.ToArray();

                            // Get the field as string, depending on the encoding
                            string fieldAsString;
                            switch (encoding)
                            {
                                case RecordCharacterEncoding.Marc:
                                    fieldAsString = ConvertMarcBytesToUnicodeString(fieldAsByteArray.ToList().AsReadOnly());
                                    break;

                                default:
                                    fieldAsString = Encoding.UTF8.GetString(fieldAsByteArray);
                                    break;
                            }

                            // Clear the byte field builder (create new memory stream)
                            byteFieldBuilder = new MemoryStream();

                            // Add the field to the list of variable data
                            fieldDatas.Add((short)startIndex,
                                new ParserVariableFieldData((short)startIndex, fieldAsString));

                            // This may be the last field, so save this index
                            lastFieldStartIndex = (short)startIndex;

                            // Save the count as the next start index
                            startIndex = count + 1;
                        }
                        else
                        {
                            // Save this byte
                            byteFieldBuilder.WriteByte((byte)result);
                        }

                        // Read the next character
                        result = _reader.ReadByte();
                        count++;
                    }

                    // Now, step through the directory, retrieve each pre-converted field data,
                    // and finish parsing
                    int directoryErrorCorrection = 0;
                    foreach (ParserDirectoryEntry directoryEntry in directoryEntries)
                    {
                        // Get the field
                        if (!fieldDatas.ContainsKey((short)(directoryEntry.StartingPosition + directoryErrorCorrection)))
                        {
                            while (
                                (!fieldDatas.ContainsKey(
                                    (short)(directoryEntry.StartingPosition + directoryErrorCorrection))) &&
                                (lastFieldStartIndex > directoryEntry.StartingPosition + directoryErrorCorrection))
                            {
                                directoryErrorCorrection += 1;
                            }

                            // If this still didn't work, throw the exception
                            if (
                                !fieldDatas.ContainsKey(
                                    (short)(directoryEntry.StartingPosition + directoryErrorCorrection)))
                            {
                                throw new ApplicationException("Field indexes and directory information cannot be resolved with one another.");
                            }
                        }
                        var fieldData = fieldDatas[(short)(directoryEntry.StartingPosition + directoryErrorCorrection)];
                        var variableFieldData = fieldData.FieldData;

                        // See if this row has an indicator
                        var indicator = "";
                        if ((variableFieldData.Length > 3) && (variableFieldData[2] == (UnitSeperator)))
                        {
                            indicator = variableFieldData.Substring(0, 2);
                            variableFieldData = variableFieldData.Substring(2);
                        }
                        else
                            variableFieldData = variableFieldData.Substring(0);

                        // Is this split into seperate subfields?
                        if ((variableFieldData.Length > 1) && (variableFieldData[0] == (UnitSeperator)))
                        {
                            // Split this into subfields
                            var subfields = variableFieldData.Substring(1).Split(UnitSeperator);

                            // Create the new field
                            var newField = new MarcField
                            {
                                Tag = Convert.ToInt32(directoryEntry.Tag),
                                Indicators = indicator
                            };

                            // Step through each subfield
                            foreach (string thisSubfield in subfields)
                            {
                                // Add this subfield
                                newField.Add_Subfield(thisSubfield[0], thisSubfield.Substring(1));
                            }

                            // Add this entry to the current record
                            thisRecord.AddField(newField);
                        }
                        else
                        {
                            // Must be just one subfield
                            thisRecord.AddField(Convert.ToInt32(directoryEntry.Tag), variableFieldData);
                        }
                    }

                    // if this was MARC8 encoding originally, change the encoding specified in the 
                    // leader, since this was converted to Unicode
                    if (encoding == RecordCharacterEncoding.Marc)
                    {
                        thisRecord.Leader = thisRecord.Leader.Substring(0, 9) + "a" + thisRecord.Leader.Substring(10);
                    }
                }
                catch (EndOfStreamException)
                {
                    throw new ApplicationException("Unexpected end of stream encountered!  Input stream may be invalid format or truncated.");
                }
                return true;
            }
        }

        private static string ConvertMarcBytesToUnicodeString(ReadOnlyCollection<byte> input)
        {
            var tmp = Encoding.Default.GetString(input.ToArray());
            return tmp;
            /*
            // Create the string builder to build the array
            var builder = new StringBuilder(input.Count);

            // Step through all the bytes in the array
            foreach (var t in input)
            {
                // If any previous bytes, save them

                // Get this byte frmo the array
                var marcByte1 = (int)t;

                builder.Append((char)marcByte1);
            }

            // Return the string
            return builder.ToString();
            */
        }

        #endregion
    }
}