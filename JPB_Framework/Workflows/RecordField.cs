using System;

namespace JPB_Framework.Workflows
{
    public class RecordField
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string PreviousValue { get; set; }
        public string RecordViewPageFieldValue { get { return RecordViewPageFieldValueFunc(); } }
        public bool RecordViewPageIsFieldVisible { get { return RecordViewPageIsFieldVisibleFunc(); } }
        private Func<string> RecordViewPageFieldValueFunc { get; set; }
        private Func<bool> RecordViewPageIsFieldVisibleFunc { get; set; }

        public RecordField(string label, string value, Func<string> recordViewPageFieldValueFunc, Func<bool> recordViewPageIsFieldVisibleFunc)
        {
            Label = label;
            Value = value;
            RecordViewPageFieldValueFunc = recordViewPageFieldValueFunc;
            RecordViewPageIsFieldVisibleFunc = recordViewPageIsFieldVisibleFunc;
        }
    }
}