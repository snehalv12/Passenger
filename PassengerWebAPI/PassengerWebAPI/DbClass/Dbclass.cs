using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using PassengerWebAPI.Models;

namespace PassengerWebAPI.DbClass
{
    public class Dbclass
    {
        public string GetDate(DateTime date)
        {
            if (date == null)
            {
                return string.Empty;
            }
            return String.Format("{0:yyyy-MM-dd}", date);
        }

        public int RegisterEvent(RegEvent EventRegistration)
        {
            int EventRegId = 0;

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                {
                    var cmd = "INSERT INTO RegEvent (Client,ProcessType,Location,DepartDate,ArrivalDate,City,LocationID,Source)VALUES (@Client,@ProcessType,@Location,@DepartDate,@ArrivalDate,@City,@LocationID,@Source);SELECT CAST(scope_identity() AS int)";
                    using (var insertCommand = new SqlCommand(cmd, con))
                    {
                        insertCommand.Parameters.AddWithValue("@Client", EventRegistration.Client);
                        insertCommand.Parameters.AddWithValue("@ProcessType", EventRegistration.ProcessType);
                        insertCommand.Parameters.AddWithValue("@Location", EventRegistration.Location);
                        insertCommand.Parameters.AddWithValue("@DepartDate", GetDate(EventRegistration.DepartDate));
                        insertCommand.Parameters.AddWithValue("@ArrivalDate", GetDate(EventRegistration.ArrivalDate));
                        insertCommand.Parameters.AddWithValue("@City", EventRegistration.City);
                        insertCommand.Parameters.AddWithValue("@LocationID", EventRegistration.LocationID);
                        insertCommand.Parameters.AddWithValue("@Source", EventRegistration.Source);
                        con.Open();
                        EventRegId = (int)insertCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return EventRegId;
        }

        public bool RegisterGuest(RegGuest GuestRegistration)
        {
            int EventGuest = 0;

            try
            {
                if (GuestRegistration.Segments.Count > 0)
                {
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                    {
                        var cmd = "INSERT INTO RegGuest" +
                            "(Client,ProcessType,EventId,FirstName,LastName,MiddleName,EmailAddress,Phone,NumberOfBags,Status,Gender,BirthDate,PassportCitizenship,PassportNumber,PassportExpiration,PassportCountry,Opt1,FirstNameAlias,LastNameAlias,Eligible,PartyGroupID,ArrivalDate,DepartDate,DepartGroup,GuestType,Segments)"
                            + "VALUES (@Client,@ProcessType,@EventId,@FirstName,@LastName,@MiddleName,@EmailAddress,@Phone,@NumberOfBags,@Status,@Gender,@BirthDate,@PassportCitizenship,@PassportNumber,@PassportExpiration,@PassportCountry,@Opt1,@FirstNameAlias,@LastNameAlias,@Eligible,@PartyGroupID,@ArrivalDate,@DepartDate,@DepartGroup,@GuestType,@Segments);SELECT CAST(scope_identity() AS int)";
                        using (var insertCommand = new SqlCommand(cmd, con))
                        {
                            insertCommand.Parameters.AddWithValue("@Client", GuestRegistration.Client);
                            insertCommand.Parameters.AddWithValue("@ProcessType", GuestRegistration.ProcessType);
                            insertCommand.Parameters.AddWithValue("@EventId", GuestRegistration.EventId);
                            insertCommand.Parameters.AddWithValue("@DepartDate", GetDate(GuestRegistration.DepartDate));
                            insertCommand.Parameters.AddWithValue("@ArrivalDate", GetDate(GuestRegistration.ArrivalDate));
                            insertCommand.Parameters.AddWithValue("@FirstName", GuestRegistration.FirstName);
                            insertCommand.Parameters.AddWithValue("@LastName", GuestRegistration.LastName);
                            insertCommand.Parameters.AddWithValue("@MiddleName", GuestRegistration.MiddleName);
                            insertCommand.Parameters.AddWithValue("@EmailAddress", GuestRegistration.EmailAddress);
                            insertCommand.Parameters.AddWithValue("@Phone", GuestRegistration.Phone);
                            insertCommand.Parameters.AddWithValue("@NumberOfBags", GuestRegistration.NumberOfBags);
                            insertCommand.Parameters.AddWithValue("@Status", GuestRegistration.Status);
                            insertCommand.Parameters.AddWithValue("@Gender", GuestRegistration.Gender);
                            insertCommand.Parameters.AddWithValue("@BirthDate", GetDate(GuestRegistration.BirthDate));
                            insertCommand.Parameters.AddWithValue("@PassportCitizenship", GuestRegistration.PassportCitizenship);
                            insertCommand.Parameters.AddWithValue("@PassportNumber", GuestRegistration.PassportNumber);
                            insertCommand.Parameters.AddWithValue("@PassportExpiration", GetDate(GuestRegistration.PassportExpiration));
                            insertCommand.Parameters.AddWithValue("@PassportCountry", GuestRegistration.PassportCountry);
                            insertCommand.Parameters.AddWithValue("@Opt1", GuestRegistration.Opt1);
                            insertCommand.Parameters.AddWithValue("@FirstNameAlias", GuestRegistration.FirstNameAlias);
                            insertCommand.Parameters.AddWithValue("@LastNameAlias", GuestRegistration.LastNameAlias);
                            insertCommand.Parameters.AddWithValue("@Eligible", GuestRegistration.Eligible);
                            insertCommand.Parameters.AddWithValue("@PartyGroupID", GuestRegistration.PartyGroupID);
                            insertCommand.Parameters.AddWithValue("@DepartGroup", GuestRegistration.DepartGroup);
                            insertCommand.Parameters.AddWithValue("@GuestType", GuestRegistration.GuestType);
                            insertCommand.Parameters.AddWithValue("@Segments", GuestRegistration.Segments.Count);


                            con.Open();
                            EventGuest = (int)insertCommand.ExecuteScalar();
                        }
                        SavingSegments(EventGuest, GuestRegistration.Segments);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return EventGuest > 0;
        }

        public void SavingSegments(int eventGuestId, List<FlightSegment> segments)
        {
            try
            {
                foreach (var segment in segments)
                {
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                    {
                        var cmd = "INSERT INTO FlightSegment (EventGuestID,Carrier,FlightNumber,DepartureCity,ArrivalCity,DepartureDate,DepartureTime,PNR,Operation)VALUES (@EventGuestID,@Carrier,@FlightNumber,@DepartureCity,@ArrivalCity,@DepartureDate,@DepartureTime,@PNR,@Operation)";
                        using (var insertCommand = new SqlCommand(cmd, con))
                        {
                            insertCommand.Parameters.AddWithValue("@EventGuestID", eventGuestId);
                            insertCommand.Parameters.AddWithValue("@Carrier", segment.Carrier);
                            insertCommand.Parameters.AddWithValue("@FlightNumber", segment.FlightNumber);
                            insertCommand.Parameters.AddWithValue("@DepartureCity", segment.DepartureCity);
                            insertCommand.Parameters.AddWithValue("@ArrivalCity", segment.ArrivalCity);
                            insertCommand.Parameters.AddWithValue("@DepartureDate", GetDate(segment.DepartureDate));
                            insertCommand.Parameters.AddWithValue("@DepartureTime", segment.DepartureTime);
                            insertCommand.Parameters.AddWithValue("@PNR", segment.PNR);
                            insertCommand.Parameters.AddWithValue("@Operation", segment.Operation);
                            con.Open();
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
