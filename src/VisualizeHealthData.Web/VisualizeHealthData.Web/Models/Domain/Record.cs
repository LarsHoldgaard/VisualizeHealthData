using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Web;
using System.Xml.Linq;

namespace VisualizeHealthData.Web.Models
{



    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Record
    {
        public Record(XElement element)
        {
            sourceNameField = (string)element.Attribute("sourceName");
            startDateField =DateTime.Parse(element.Attribute("startDate").Value);
            endDateField = DateTime.Parse(element.Attribute("endDate").Value);
            typeField = (string)element.Attribute("type");
            unitField = (string)element.Attribute("unit");
            creationDateField = (DateTime)element.Attribute("creationDate");
            valueField = (string)element.Attribute("value");


            //< Record type = "HKQuantityTypeIdentifierBodyMassIndex" sourceName = "Health Mate" sourceVersion = "3030101" unit = "count" creationDate = "2018-03-25 15:49:38 +0200" startDate = "2018-03-24 08:37:30 +0200" endDate = "2018-03-24 08:37:30 +0200" value = "25.3691" >

            //< MetadataEntry key = "Health Mate App Version" value = "3.3.1" />

            //< MetadataEntry key = "Withings User Identifier" value = "3326231" />

            //< MetadataEntry key = "Modified Date" value = "2018-03-24 06:38:09 +0000" />

            //< MetadataEntry key = "Withings Link" value = "withings-bd2://timeline/measure?userid=3326231&amp;date=1521873450&amp;type=1" />

            //< MetadataEntry key = "HKWasUserEntered" value = "0" />

            //</ Record >

        }


        public RecordType? RecordType
        {
            get
            {
                switch (typeField)
                {
                    case "HKQuantityTypeIdentifierBodyMass":
                        return Models.RecordType.BodyMass;
                    case "HKQuantityTypeIdentifierStepCount":
                        return Models.RecordType.Steps;
                    case "HKQuantityTypeIdentifierFlightsClimbed":
                        return Models.RecordType.FlightsClimbed;
                    case "HKCategoryTypeIdentifierMindfulSession":
                        return Models.RecordType.Meditation;
                    case "HKCategoryTypeIdentifierSleepAnalysis":
                        return Models.RecordType.SleepAnalysis;
                    case "HKQuantityTypeIdentifierDistanceWalkingRunning":
                        return Models.RecordType.DistanceWalkCycling;
                    case "HKQuantityTypeIdentifierDistanceCycling":
                        return Models.RecordType.DistanceCycling;
                    case "HKQuantityTypeIdentifierBodyMassIndex":
                        return Models.RecordType.BodyMassIndex;


                }
                return (RecordType?) null;
            }
        }

        public RecordSource? RecordSource
        {
            get
            {
                switch (sourceNameField)
                {
                    case "Health Mate":
                        return Models.RecordSource.HealthMate;
                    case "MyFitnessPal":
                        return Models.RecordSource.MyFitnessPal;
                    case "Endomondo":
                        return Models.RecordSource.Endomondo;
                }

                if(sourceName.ToLower().Contains("phone"))
                {
                    return Models.RecordSource.Device;
                }

                return (RecordSource?)null;
            }
        }

        private RecordMetadataEntry[] metadataEntryField;

        private string typeField;

        private string sourceNameField;

        private uint sourceVersionField;

        private string unitField;

        private DateTime creationDateField;

        private DateTime startDateField;

        private DateTime endDateField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MetadataEntry")]
        public RecordMetadataEntry[] MetadataEntry
        {
            get
            {
                return this.metadataEntryField;
            }
            set
            {
                this.metadataEntryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string sourceName
        {
            get
            {
                return this.sourceNameField;
            }
            set
            {
                this.sourceNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint sourceVersion
        {
            get
            {
                return this.sourceVersionField;
            }
            set
            {
                this.sourceVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unit
        {
            get
            {
                return this.unitField;
            }
            set
            {
                this.unitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public DateTime creationDate
        {
            get
            {
                return this.creationDateField;
            }
            set
            {
                this.creationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public DateTime startDate
        {
            get
            {
                return this.startDateField;
            }
            set
            {
                this.startDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public DateTime endDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RecordMetadataEntry
    {

        private string keyField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }


}