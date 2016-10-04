using System;

namespace JPB_Framework.Workflows
{
    public class RecordField
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string PreviousValue { get; set; }
        public string RecordViewPageFieldValue => RecordViewPageFieldValueFunc();
        public bool RecordViewPageIsFieldVisible => RecordViewPageIsFieldVisibleFunc();
        private Func<string> RecordViewPageFieldValueFunc { get; }
        private Func<bool> RecordViewPageIsFieldVisibleFunc { get; }

        public RecordField(string label, string value, Func<string> recordViewPageFieldValueFunc, Func<bool> recordViewPageIsFieldVisibleFunc)
        {
            Label = label;
            Value = value;
            RecordViewPageFieldValueFunc = recordViewPageFieldValueFunc;
            RecordViewPageIsFieldVisibleFunc = recordViewPageIsFieldVisibleFunc;
        }
    }
}