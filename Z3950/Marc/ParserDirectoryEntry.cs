namespace Z3950.Marc
{
    /// <summary> Structure stores the basic information about a directory entry when parsing
    ///  a MARC21 record </summary>
    /// <remarks> This is only used while actually parsing the MARC21 record </remarks>
    internal struct ParserDirectoryEntry
    {
        public readonly short Tag;

        public readonly short FieldLength;

        public readonly short StartingPosition;

        public ParserDirectoryEntry( short tagValue, short lengthValue, short startingPositionValue )
        {
            Tag = tagValue;
            FieldLength = lengthValue;
            StartingPosition = startingPositionValue;
        }
    }
}