using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

public class XMLParser
{
    #region Constructor
    public XMLParser()
    {

    }
    #endregion

    public DTOEvacuationList PopulateEvacuation(XDocument xDoc)
    {
        DTOEvacuationList dtoEvacuationList = new DTOEvacuationList();

        XDocument xDocument = new XDocument();
        xDocument = xDoc;

        var Evacuations = from evacuations in xDocument.Element("Location").Elements("ListOfEvacuation").Elements("Evacuation")
                         select new
                         {
                             ID = evacuations.Element("ID").Value,
                             Name = evacuations.Element("Name").Value,
                             Longtitude = evacuations.Element("Longitude").Value,
                             Latitude = evacuations.Element("Latitude").Value,
                             CurrentPeople = Convert.ToInt32(evacuations.Element("CurrentPeople").Value),
                             MaxPeople = Convert.ToInt32(evacuations.Element("MaxPeople").Value),
                             Food = Convert.ToInt32(evacuations.Element("FoodSupply").Value),
                             Water = Convert.ToInt32(evacuations.Element("WaterSupply").Value),
                             Medicine = Convert.ToInt32(evacuations.Element("MedicineSupply").Value)
                         };

        foreach (var evacuation in Evacuations)
        {

            DTOEvacuation dtoEvacuation = new DTOEvacuation
            {
                ID = Convert.ToInt32(evacuation.ID.ToString()),
                Name = evacuation.Name.ToString(),
                Longitude = evacuation.Longtitude.ToString(),
                Latitude = evacuation.Latitude.ToString(),
                CurrentPeople = evacuation.CurrentPeople,
                MaxPeople = evacuation.MaxPeople,
                Food = evacuation.Food,
                Water = evacuation.Water,
                Medicine = evacuation.Medicine
            };

            dtoEvacuationList.Add(dtoEvacuation);

        }

        return dtoEvacuationList;
    }

    public DTOMorgueList PopulateMorgue(XDocument xDoc)
    {
        DTOMorgueList dtoMorgueList = new DTOMorgueList();
        

        XDocument xDocument = new XDocument();
        xDocument = xDoc;

        var Morgues = from morgues in xDocument.Element("Location").Elements("ListOfMorgue").Elements("Morgue")
                      select new
                      {
                          ID = Convert.ToInt32(morgues.Element("ID").Value),
                          Name = morgues.Element("Name").Value,
                          ContactNumber = morgues.Element("ContactNumber").Value,
                          Longitude = morgues.Element("Longitude").Value,
                          Latitude = morgues.Element("Latitude").Value,
                          TotalBodies = Convert.ToInt32(morgues.Element("TotalBodies").Value),
                          TotalMales = Convert.ToInt32(morgues.Element("TotalMale").Value),
                          TotalFemales = Convert.ToInt32(morgues.Element("TotalFemale").Value),
                          TotalTeens = Convert.ToInt32(morgues.Element("TotalTeen").Value),
                          TotalChilds = Convert.ToInt32(morgues.Element("TotalTeen").Value),
                          TotalAdults = Convert.ToInt32(morgues.Element("TotalAdult").Value),
                          TotalIdentified = Convert.ToInt32(morgues.Element("TotalIdentified").Value),
                          TotalUnIdentified = Convert.ToInt32(morgues.Element("TotalUnIdentified").Value),
                          PersonList = from person in morgues.Elements("Person")
                                       select new
                                       {
                                           ID = Convert.ToInt32(person.Element("ID").Value),
                                           Identified = Convert.ToBoolean(person.Element("Identified").Value),
                                           Name = person.Element("Name").Value,
                                           Gender = person.Element("Gender").Value,
                                           AgeBracket = person.Element("AgeBracket").Value                                            
                                       }
                      };

        foreach (var morgue in Morgues)
        {
             DTOMorgue dtoMorgue = new DTOMorgue
            {

                ID = morgue.ID,
                Name = morgue.Name,
                ContactNumber = morgue.ContactNumber,
                Latitude = morgue.Latitude,
                Longitude = morgue.Longitude,
                TotalBodies = morgue.TotalBodies,
                TotalMales = morgue.TotalMales,
                TotalFemales = morgue.TotalFemales,
                TotalTeens = morgue.TotalTeens,
                TotalAdults = morgue.TotalAdults,
                TotalChilds = morgue.TotalChilds,
                TotalIdentified = morgue.TotalIdentified,
                TotalUnidentified = morgue.TotalUnIdentified,
                PersonList = (DTOPersonList)morgue.PersonList
            };

             //DTOPersonList dtoPersonList = new DTOPersonList();

            //var Persons = from person in xDocument.Element("Location").Elements("ListOfMorgue").Elements("Morgue").Elements("Person")
            //               select new
            //               {
            //                   ID = Convert.ToInt32(person.Element("ID").Value),
            //                   Name = person.Element("Name").Value,
            //                   Identified = Convert.ToBoolean(person.Element("Identified").Value),
            //                   Gender = person.Element("Gender").Value,
            //                   AgeBracket = person.Element("AgeBracket").Value
                    
            //               };

            //foreach (var person in Persons)
            //{
            //    DTOPerson dtoPerson = new DTOPerson
            //    {
            //         ID = person.ID,
            //         Name = person.Name,
            //         Gender = person.Gender,
            //         AgeBracket = person.AgeBracket,
            //         Identified = person.Identified
            //    };

            //    dtoPersonList.Add(dtoPerson);
            //}

            //dtoMorgue.PersonList = dtoPersonList;
            dtoMorgueList.Add(dtoMorgue);

        }
        return dtoMorgueList;
    }


}
