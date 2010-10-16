using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TransformingDataWithLinq
{
    [TestFixture]
    public class GarfieldTranslations
    {
        [Test]
        public void GetTranslations()
        {
            var dal = new NetworkTranslationDAL();
            var translations = dal.GetTranslations();
            Assert.AreEqual(3, translations.Count);
        }
    }

    public class NetworkTranslationDAL
    {
        public IList<NetworkTranslation> GetTranslations()  //This is how the data is coming from the database.  
        {
            IList<Translation> transList = new List<Translation>
            {
                new Translation { TargetID = 1, TransFieldID = 1, RuleType = "IN", FieldDescription="Network", RuleValue="ANN"},
                new Translation { TargetID = 1, TransFieldID = 2, RuleType = "IN", FieldDescription="Role", RuleValue="*"},
                new Translation { TargetID = 1, TransFieldID = 3, RuleType = "EX", FieldDescription="Written Agreement", RuleValue="MGA1"},
                new Translation { TargetID = 1, TransFieldID = 3, RuleType = "EX", FieldDescription="Written Agreement", RuleValue="S"},
                new Translation { TargetID = 1, TransFieldID = 3, RuleType = "IN", FieldDescription="Written Agreement", RuleValue="*"},
                new Translation { TargetID = 1, TransFieldID = 5, RuleType = "IN", FieldDescription="Provider Type", RuleValue="*"},
                new Translation { TargetID = 1, TransFieldID = 6, RuleType = "IN", FieldDescription="Degree", RuleValue="*"},
                new Translation { TargetID = 1, TransFieldID = 7, RuleType = "Ex", FieldDescription="Specialty", RuleValue="*"},
                new Translation { TargetID = 1, TransFieldID = 7, RuleType = "IN", FieldDescription="Specialty", RuleValue="*"},
                new Translation { TargetID = 1, TransFieldID = 8, RuleType = "IN", FieldDescription="County", RuleValue="*"},
                new Translation { TargetID = 1, TransFieldID = 9, RuleType = "IN", FieldDescription="State", RuleValue="*"},
                new Translation { TargetID = 1, TransFieldID = 11, RuleType = "IN", FieldDescription="Tax ID", RuleValue="*"},

                new Translation { TargetID = 2, TransFieldID = 1, RuleType = "IN", FieldDescription="Network", RuleValue="ANN"},
                new Translation { TargetID = 2, TransFieldID = 2, RuleType = "IN", FieldDescription="Role", RuleValue="*"},
                new Translation { TargetID = 2, TransFieldID = 3, RuleType = "EX", FieldDescription="Written Agreement", RuleValue="MGA1"},
                new Translation { TargetID = 2, TransFieldID = 3, RuleType = "EX", FieldDescription="Written Agreement", RuleValue="S"},
                new Translation { TargetID = 2, TransFieldID = 3, RuleType = "IN", FieldDescription="Written Agreement", RuleValue="*"},
                new Translation { TargetID = 2, TransFieldID = 5, RuleType = "IN", FieldDescription="Provider Type", RuleValue="*"},
                new Translation { TargetID = 2, TransFieldID = 6, RuleType = "IN", FieldDescription="Degree", RuleValue="*"},
                new Translation { TargetID = 2, TransFieldID = 7, RuleType = "Ex", FieldDescription="Specialty", RuleValue="*"},
                new Translation { TargetID = 2, TransFieldID = 7, RuleType = "IN", FieldDescription="Specialty", RuleValue="*"},
                new Translation { TargetID = 2, TransFieldID = 8, RuleType = "IN", FieldDescription="County", RuleValue="*"},
                new Translation { TargetID = 2, TransFieldID = 9, RuleType = "IN", FieldDescription="State", RuleValue="*"},
                new Translation { TargetID = 2, TransFieldID = 11, RuleType = "IN", FieldDescription="Tax ID", RuleValue="*"},

                new Translation { TargetID = 3, TransFieldID = 1, RuleType = "IN", FieldDescription="Network", RuleValue="ANN"},
                new Translation { TargetID = 3, TransFieldID = 2, RuleType = "IN", FieldDescription="Role", RuleValue="*"},
                new Translation { TargetID = 3, TransFieldID = 3, RuleType = "EX", FieldDescription="Written Agreement", RuleValue="MGA1"},
                new Translation { TargetID = 3, TransFieldID = 3, RuleType = "EX", FieldDescription="Written Agreement", RuleValue="S"},
                new Translation { TargetID = 3, TransFieldID = 3, RuleType = "IN", FieldDescription="Written Agreement", RuleValue="*"},
                new Translation { TargetID = 3, TransFieldID = 5, RuleType = "IN", FieldDescription="Provider Type", RuleValue="*"},
                new Translation { TargetID = 3, TransFieldID = 6, RuleType = "IN", FieldDescription="Degree", RuleValue="*"},
                new Translation { TargetID = 3, TransFieldID = 7, RuleType = "Ex", FieldDescription="Specialty", RuleValue="*"},
                new Translation { TargetID = 3, TransFieldID = 7, RuleType = "IN", FieldDescription="Specialty", RuleValue="*"},
                new Translation { TargetID = 3, TransFieldID = 8, RuleType = "IN", FieldDescription="County", RuleValue="*"},
                new Translation { TargetID = 3, TransFieldID = 9, RuleType = "IN", FieldDescription="State", RuleValue="*"},
                new Translation { TargetID = 3, TransFieldID = 11, RuleType = "IN", FieldDescription="Tax ID", RuleValue="*"}
            };

            IList<NetworkTranslation> result = new List<NetworkTranslation>();

            //NetworkTranslation xpfTrans = new NetworkTranslation();  //I figured I'd create the object I want to return
            //IList<KeyValuePair<string, string>> listWrittenAgreements = new List<KeyValuePair<string, string>>();  //this represents the multiple written agreements above that I want in a list to add to xpfTrans object.
            //IList<KeyValuePair<string, string>> listSpecialties = new List<KeyValuePair<string, string>>();  //this represents the specialties above that I want in a list to add to xpfTrans object.

            //TODO:Figure out how to move the data from transList to NetworkTranslation

            foreach (var xpfTrans in transList.GroupBy(t => t.TargetID).Select(g => new NetworkTranslation()))
            {
                result.Add(xpfTrans);
            }

            return result;
        }

        public class Translation //this is a temporary class I'm using to hold the data to show how it is coming from the database.
        {
            public int TargetID { get; set; }
            public int TransFieldID { get; set; }
            public string RuleType { get; set; }
            public string FieldDescription { get; set; }
            public string RuleValue { get; set; }
        }

        public class NetworkTranslation  //This is the way I want the data to be returned to my presentation layer.
        {
            public int TargetId { get; set; }
            public string PimsNetwork { get; set; }
            public string PimsRole { get; set; }
            public IList<KeyValuePair<string, string>> WrittenAgreement { get; set; }
            public string ProviderType { get; set; }
            public string Degree { get; set; }
            public IList<KeyValuePair<string, string>> Specialty { get; set; }
            public string County { get; set; }
            public string State { get; set; }
            public string TaxID { get; set; }
        }
    }
}