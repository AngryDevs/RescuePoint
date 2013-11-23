using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

public class XMLParser
{
    #region Constructor
    public XMLParser()
    {

    }
    #endregion
    public DTOEvacuationList PopulateEvacuation(XmlDocument xmlDoc)
    {
        DTOEvacuationList dtoEvacuationList = new DTOEvacuationList();

        #region Variables

        XmlDocument XmlDoc = new XmlDocument();

        XmlNode listOfEvacuation;
        XmlNodeList Evacuations;

        #endregion

        XmlDoc = xmlDoc;

        listOfEvacuation = XmlDoc.SelectSingleNode("//ListOfEvacuation");
        Evacuations = listOfEvacuation.SelectNodes("Evacuation");

        foreach (XmlNode evacuation in Evacuations)
        {

            DTOEvacuation dtoEvacuation = new DTOEvacuation
            {
                ID = Convert.ToInt32(evacuation.SelectSingleNode("ID").InnerText),
                Name = evacuation.SelectSingleNode("Name").InnerText,
                Longtitude = evacuation.SelectSingleNode("Longitude").InnerText,
                Latitude = evacuation.SelectSingleNode("Latitude").InnerText,
                CurrentPeople = Convert.ToInt32(evacuation.SelectSingleNode("CurrentPeople").InnerText),
                MaxPeople = Convert.ToInt32(evacuation.SelectSingleNode("MaxPeople").InnerText),
                Food = Convert.ToInt32(evacuation.SelectSingleNode("FoodSupply").InnerText),
                Water = Convert.ToInt32(evacuation.SelectSingleNode("WaterSupply").InnerText),
                Medicine = Convert.ToInt32(evacuation.SelectSingleNode("MedicineSupply").InnerText)
            };

            dtoEvacuationList.Add(dtoEvacuation);
        }
        
        return dtoEvacuationList;
    }

    public DTOMorgueList PopulateMorgue(XmlDocument xmlDoc)
    {
       

        #region Variables 

        DTOMorgueList dtoMorgueList = new DTOMorgueList();

        XmlDocument XmlDoc = new XmlDocument();
        
        XmlNode listofMorgue;
        XmlNodeList Morgues;


        #endregion

        

        XmlDoc = xmlDoc;

        listofMorgue = XmlDoc.SelectSingleNode("//ListOfMorgue");
        Morgues = listofMorgue.SelectNodes("Morgue");

        foreach (XmlNode morgue in Morgues)
        {
            DTOMorgue dtoMorgue = new DTOMorgue
            {

                ID = Convert.ToInt32(morgue.SelectSingleNode("ID").InnerText),
                Name = morgue.SelectSingleNode("Name").InnerText,
                ContactNumber = morgue.SelectSingleNode("ContactNumber").InnerText,
                Latitude = morgue.SelectSingleNode("Latitude").InnerText,
                Longtitude = morgue.SelectSingleNode("Longitude").InnerText,
                TotalBodies = morgue.SelectSingleNode("TotalBodies").InnerText,
                TotalMale = morgue.SelectSingleNode("TotalMale").InnerText,
                TotalFemale = morgue.SelectSingleNode("TotalFemale").InnerText,
                TotalTeen = morgue.SelectSingleNode("TotalTeen").InnerText,
                TotalAdult = morgue.SelectSingleNode("TotalAdult").InnerText,
                TotalChild = morgue.SelectSingleNode("TotalChild").InnerText
            };

            XmlNodeList Persons = morgue.SelectNodes("Person");

            DTOPersonList dtoPersonList = new DTOPersonList();
                
            foreach (XmlNode person in Persons)
            {
                DTOPerson dtoPerson = new DTOPerson
                {
                    ID = Convert.ToInt32(person.SelectSingleNode("ID").InnerText),
                    Name = person.SelectSingleNode("Name").InnerText,
                    Gender = person.SelectSingleNode("Gender").InnerText,
                    AgeBracket = person.SelectSingleNode("AgeBracket").InnerText,
                    Identified = Convert.ToBoolean(person.SelectSingleNode("Identified").InnerText)
                };

                dtoPersonList.Add(dtoPerson);

                
            }

            dtoMorgue.PersonList = dtoPersonList;
            dtoMorgueList.Add(dtoMorgue);
        }


        return dtoMorgueList;
    }


}
