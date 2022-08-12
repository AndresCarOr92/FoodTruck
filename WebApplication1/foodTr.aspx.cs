/*Esto es el back-end del proyecto*/

//Estas son librerias para la manipulación de la información con C#
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;

namespace WebApplication1
{
    public partial class foodTr : System.Web.UI.Page
    {
        /*este es el método que se ejecuta en cuanto se inicia la pagina*/
        protected void Page_Load(object sender, EventArgs e)
        {
            getDataAPI();
        }
        /*este método permite inicialmente consultar a la API de FoodTrack SF y 
         * pintar la tabla de datos*/
        public void getDataAPI()
        {
            string jsonString;
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
            //Api FoodTrack SF
            string apiFoodTrucks = "https://data.sfgov.org/resource/rqzj-sfat.json";           
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(apiFoodTrucks));
            WebReq.Method = "GET";
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);

                jsonString = reader.ReadToEnd();
            }
            var myObject = Welcome.FromJson(jsonString);
            dt.Rows.Clear();
            Label2.Text = "Food Trucks:"+ myObject.Count;
            sb.Append("<table >");
            for (int i = 0; i < myObject.Count; i++)
            {
                sb.Append("<tr>");
                  sb.Append("<th style='border-bottom:inset; '>");
                     sb.Append("<label style='color:Tomato;font-weight:bold;font-size:19px;font-family: system-ui;'>Type: " + myObject[i].Facilitytype.ToString() + " </label><br/>");
                     sb.Append("<label style='color:Orange;font-size:12px;font-family: system-ui;' >Business name: " + myObject[i].Applicant.ToString() + " </label><br/>");
                if (myObject[i].Fooditems is null)
                {
                    sb.Append("<label style='color:#ab5940;font-size:11px;font-family: system-ui;'>Food item: Sin Info</label><br/>");
                }
                else {
                    sb.Append("<label style='color:#ab5940;font-size:11px;font-family: system-ui;'>Food item: " + myObject[i].Fooditems.ToString() + "</label><br/>");
                    
                }                 
                     sb.Append("<label style='color:MediumSeaGreen;font-size:11px;font-family: system-ui;' >Adress: " + myObject[i].Address.ToString() + "</label><br/>");
                     sb.Append("<center> <button type='button' runat='server' class='button' onclick='fnNotificacion(" + myObject[i].Latitude.ToString() + "," + myObject[i].Longitude.ToString() + ")'> <img src='img/food1.png' Width='36px' Height='32px'/>&nbsp;Go!</button></center></br>");
        
                sb.Append("</th>");
                sb.Append("</tr>");
            }
            Literal1.Text = sb.ToString();

            }
            catch (Exception ex)
            {
                string script = "alert('Se produjo el siguiente error: " + ex + "');"; ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
        }
        /*Este es un Servicio Web que permite interactuar entre javascript metodo 
         * con el servicio de GOOGLE MAP y la información del API*/
        [System.Web.Services.WebMethod]
        public static List<Welcome> getData(string tipo){
            foodTr m = new foodTr();
            List<Welcome> list = m.listData(tipo);
            return list;
        }
        /*Este es un método que hace la consulta y filtra la información por tipo del Api */
        public List<Welcome> listData(string tipo)
        {           
            List<Welcome> lista = new List<Welcome>();
            string jsonString;
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            //Api FoodTrack SF
            string apiFoodTrucks = "https://data.sfgov.org/resource/rqzj-sfat.json?$where=starts_with(Facilitytype,'" + tipo + "')";
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(apiFoodTrucks));
            WebReq.Method = "GET";
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);

                jsonString = reader.ReadToEnd();
            }
            var myObject = Welcome.FromJson(jsonString);
            dt.Rows.Clear();
    
            for (int i = 0; i < myObject.Count; i++)
            {
                Welcome lis = new Welcome();
                lis.Latitude = myObject[i].Latitude;
                lis.Longitude = myObject[i].Longitude;
                lis.Facilitytype = myObject[i].Facilitytype;
                lis.Applicant = myObject[i].Applicant;
                if (myObject[i].Fooditems is null)
                {
                    lis.Fooditems = "Sin Info";
                }
                else
                {
                    lis.Fooditems = myObject[i].Fooditems;
                }
                lis.Address = myObject[i].Address.ToString();                
                lista.Add(lis);
            }
            return lista;
        }


    }
    /*Esto es una clase Welcom, que permite realizar peticiones de informacion al Api*/
    public partial class Welcome
    {
        [JsonProperty("objectid")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Objectid { get; set; }

        [JsonProperty("applicant")]
        public string Applicant { get; set; }

        [JsonProperty("facilitytype", NullValueHandling = NullValueHandling.Ignore)]
        public Facilitytype? Facilitytype { get; set; }

        [JsonProperty("cnn")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Cnn { get; set; }

        [JsonProperty("locationdescription", NullValueHandling = NullValueHandling.Ignore)]
        public string Locationdescription { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("blocklot", NullValueHandling = NullValueHandling.Ignore)]
        public string Blocklot { get; set; }

        [JsonProperty("block", NullValueHandling = NullValueHandling.Ignore)]
        public string Block { get; set; }

        [JsonProperty("lot", NullValueHandling = NullValueHandling.Ignore)]
        public string Lot { get; set; }

        [JsonProperty("permit")]
        public string Permit { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("fooditems", NullValueHandling = NullValueHandling.Ignore)]
        public string Fooditems { get; set; }

        [JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
        public string X { get; set; }

        [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
        public string Y { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("schedule")]
        public Uri Schedule { get; set; }

        [JsonProperty("approved", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Approved { get; set; }

        [JsonProperty("received")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Received { get; set; }

        [JsonProperty("priorpermit")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Priorpermit { get; set; }

        [JsonProperty("expirationdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Expirationdate { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty(":@computed_region_yftq_j783", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ComputedRegionYftqJ783 { get; set; }

        [JsonProperty(":@computed_region_p5aj_wyqh", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ComputedRegionP5AjWyqh { get; set; }

        [JsonProperty(":@computed_region_rxqg_mtj9", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ComputedRegionRxqgMtj9 { get; set; }

        [JsonProperty(":@computed_region_bh8s_q3mv", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ComputedRegionBh8SQ3Mv { get; set; }

        [JsonProperty(":@computed_region_fyvs_ahh9", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ComputedRegionFyvsAhh9 { get; set; }

        [JsonProperty("dayshours", NullValueHandling = NullValueHandling.Ignore)]
        public string Dayshours { get; set; }

        [JsonProperty("noisent", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Noisent { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("human_address")]
        public HumanAddress HumanAddress { get; set; }
    }

    public enum Facilitytype { PushCart, Truck };

    public enum HumanAddress { AddressCityStateZip };

    public enum Status { Approved, Expired, Issued, Requested, Suspend };

    public partial class Welcome
    {
        public static List<Welcome> FromJson(string json) => JsonConvert.DeserializeObject<List<Welcome>>(json, WebApplication1.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Welcome> self) => JsonConvert.SerializeObject(self, WebApplication1.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                FacilitytypeConverter.Singleton,
                HumanAddressConverter.Singleton,
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class FacilitytypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Facilitytype) || t == typeof(Facilitytype?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Push Cart":
                    return Facilitytype.PushCart;
                case "Truck":
                    return Facilitytype.Truck;
            }
            throw new Exception("Cannot unmarshal type Facilitytype");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Facilitytype)untypedValue;
            switch (value)
            {
                case Facilitytype.PushCart:
                    serializer.Serialize(writer, "Push Cart");
                    return;
                case Facilitytype.Truck:
                    serializer.Serialize(writer, "Truck");
                    return;
            }
            throw new Exception("Cannot marshal type Facilitytype");
        }

        public static readonly FacilitytypeConverter Singleton = new FacilitytypeConverter();
    }

    internal class HumanAddressConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(HumanAddress) || t == typeof(HumanAddress?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "{\"address\": \"\", \"city\": \"\", \"state\": \"\", \"zip\": \"\"}")
            {
                return HumanAddress.AddressCityStateZip;
            }
            throw new Exception("Cannot unmarshal type HumanAddress");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (HumanAddress)untypedValue;
            if (value == HumanAddress.AddressCityStateZip)
            {
                serializer.Serialize(writer, "{\"address\": \"\", \"city\": \"\", \"state\": \"\", \"zip\": \"\"}");
                return;
            }
            throw new Exception("Cannot marshal type HumanAddress");
        }

        public static readonly HumanAddressConverter Singleton = new HumanAddressConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "APPROVED":
                    return Status.Approved;
                case "EXPIRED":
                    return Status.Expired;
                case "ISSUED":
                    return Status.Issued;
                case "REQUESTED":
                    return Status.Requested;
                case "SUSPEND":
                    return Status.Suspend;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.Approved:
                    serializer.Serialize(writer, "APPROVED");
                    return;
                case Status.Expired:
                    serializer.Serialize(writer, "EXPIRED");
                    return;
                case Status.Issued:
                    serializer.Serialize(writer, "ISSUED");
                    return;
                case Status.Requested:
                    serializer.Serialize(writer, "REQUESTED");
                    return;
                case Status.Suspend:
                    serializer.Serialize(writer, "SUSPEND");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
