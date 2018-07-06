namespace Z3950.Marc
{
    /// <summary> Holds the data about a single subfield in a <see cref="MarcField"/>. <br /> <br /> </summary>
    public class MarcSubfield
    {
        public MarcSubfield(char subfieldCode, string data)
        {
            // Save the parameters
            SubfieldCode = subfieldCode;
            Data = data;
        }
       
        public char SubfieldCode { get; private set; }

        public string Data { get; set; }

        /// <summary> Returns this MARC Subfield as a string </summary>
        /// <returns> Subfield in format '|x data'.</returns>
        public override string ToString()
        {
            return "|" + SubfieldCode + " " + Data;
        }
    }
}