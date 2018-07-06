
namespace Z3950.Marc
{
    internal struct ParserVariableFieldData
    {
        public readonly string FieldData;

        public readonly short StartingPosition;

        public ParserVariableFieldData(short startingPositionValue, string fieldDataValue)
        {
            StartingPosition = startingPositionValue;
            FieldData = fieldDataValue;
        }
    }
}