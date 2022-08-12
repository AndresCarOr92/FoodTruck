 <!-- esta es la vista principal, para el Reto  es un proyecto web -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="foodTr.aspx.cs" Inherits="WebApplication1.foodTr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0"/>

    <title>Reto Food Truck</title>
    <script src="js/jquery-3.6.0.min.js"></script>
    <script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script> 
    <script async="" defer="" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC8NWbW83LD3eZV0d_bqBEL6UacS1Z3j3A&amp;callback=initMap">
    </script>
     <!-- Aqui conectamos con el servicio  de Google para pintar el mapa-->
    <script>
        //funcion que inicia el mapa
	 function initMap(){
         var locations = [
           [MODULO='SF',LATITUD=37.773972,LONGITUD=-122.431297]
          
         ];
          var uluru = {lat:37.773972, lng:-122.431297};
        				var map = new google.maps.Map(document.getElementById('map'), {
          				zoom: 9,
          				center: uluru
        				});
        
						var infowindow = new google.maps.InfoWindow();
						var marker, i;
		
						for (i = 0; i < locations.length; i++) {
			 				marker = new google.maps.Marker({
        					position: new google.maps.LatLng(locations[i][1], locations[i][2]),
					        map: map
      						});
							google.maps.event.addListener(marker, 'click', (function(marker, i) {
        					return function() {
					          infowindow.setContent(locations[i][0]);	
							  infowindow.open(map, marker);
        										}
      					})(marker, i));
							}
        } 
        //funcion cuando pulsamos el boton Go!
     function fnNotificacion(lati, long){
            document.getElementById('total').innerHTML = "";       
            var uluru = {lat:lati, lng:long};
        	var map = new google.maps.Map(document.getElementById('map'), {
          	zoom: 18,
          	center: uluru
        	});
        
			var infowindow = new google.maps.InfoWindow();
			var marker, i;					
			 	marker = new google.maps.Marker({
                position: new google.maps.LatLng(lati, long),
                icon: "img/pin-02.png",
				map: map
      			});
				google.maps.event.addListener(marker, 'click', (function(marker, i) {
        		return function() {
					infowindow.setContent("Food Truck");	
					infowindow.open(map, marker);
        							}
      		})(marker, i));
							
     }
        //funcion que filtra por tipo
     function filtroTipo() { 
         document.getElementById('total').innerHTML = "";
         var e = document.getElementById("typeFood");
         var value = e.value;                   
            $.ajax({                
                url: "foodTr.aspx/getData",
                type: "post",
                data: "{'tipo':'" + value + "'}",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (data) {  
                   
                    var data = data.d;
                    var data_length = data.length;
                    var locations = [data_length];
                    for (var i = 0; i <data_length; i++) {                        
                        locations[i] = {LATITUD: data[i]["Latitude"], LONGITUD: data[i]["Longitude"] , Tipo: data[i]["Facilitytype"], Applicant: data[i]["Applicant"], Fooditems: data[i]["Fooditems"], Address: data[i]["Address"]};
                    } 
                    var uluru = {lat:37.773972, lng:-122.431297};
        			var map = new google.maps.Map(document.getElementById('map'), {
          			zoom: 13,
          			center: uluru
        			});
                    var infowindow = new google.maps.InfoWindow();
					var marker, i;
                    
                    document.getElementById('total').innerHTML = "Fine: "+locations.length;
                    for (i = 0; i < locations.length; i++) {
                             
			 				marker = new google.maps.Marker({
                                    position: new google.maps.LatLng(parseFloat(locations[i].LATITUD), parseFloat(locations[i].LONGITUD)),
                                    icon: "img/pin-02.png",
                                    map: map
      						});
							google.maps.event.addListener(marker, 'click', (function(marker, i) {
                                return function () {
                                    infowindow.setContent(locations[i].Applicant);	
							        infowindow.open(map, marker);
        										}
      				    	})(marker, i));
					    }
      				
                },//cierre success: function (data) {
                error: function () {
                    alert("Ha ocurrido un error, favor de filtrar.");
                }//cierre success: function (data) {
            });///cierre $.ajax

        };
    </script>
     <!--Estilos  -->
    <style>
        
   .button {
    background-color: #75b371;
    border: none;
    color: white;
    text-align: center;
    display: inline-block;
    font-size: 11px;
    margin: 4px 2px;
    cursor: pointer;
    width: 100px;
    height: 40px;
    font-family: system-ui;
    }
    .result .iframe-visual-update {
        z-index: 1;
    }
    .result-iframe {
    border: 0;
    width: 100%;  
    z-index: 1;
    }

    .footer {
        position: fixed;
        left: 0;
        bottom: 0;
        width: 100%;
        background-image: url(img/back.jpg);
        font-family: montserrat;
        height: 180px;
        text-align: center;
        color: #2B2B2B;
    }
       
    
    .content {
        width: 100%;
    }

     
    .header {
        overflow: hidden;
        box-shadow: 0px 3px 5px grey;
        height: 80px;
        font-family:Montserrat;
    }

    .izq {
        float: left;
        color: black;
        text-align: center;
        text-decoration: none;
        font-size: 18px;
        line-height: 25px;
        border-radius: 4px;
    }

    .header a.logo {
        font-size: 25px;
        font-weight: bold;
        vertical-align: top;
    }

    .header a:hover {
        background-color: #ddd;
        color: black;
    }

    .header a.active {
        background-color: dodgerblue;
        color: white;
    }

    .header-right {
        float: right;
    }

    .header-foto {
        width: 62px;
        height: 73px;
        border-radius: 7px;
    }

    @media screen and (max-width: 500px) {
    .header a {
        float: none;
        display: block;
        text-align: left;
    }

    .header-right {
        float: none;
    }
    }

    .nombre {
        font-weight: 300;
        color: #283747;
    }

    .separador {
        background:orangered;
    }

    .bienvenida {
        padding: 0%;
    }
    </style>
  </head>
    
  <body>
       <!-- la cabecera de la bienvenida y Titulo -->
     <header class="header"> 
        <div class="header-right">
            <table style="width:100%" border="0" class="bienvenida">
                <tbody>
                    <tr>
                      <td style="width:70%"> &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                        <asp:Image ID="Image1" runat="server" ImageUrl="img/food1.png" Width="55px"/>
                            &nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:label runat="server" text="Locating food trucks in San Francisco" Font-Bold="false" Font-Size="20pt" id="lbX" ForeColor="#ff6666"></asp:label>
                            </td>
                        <td style="width:1%;opacity: 0.2" class="separador" >&nbsp;</td>
                        <td style="width:3%">&nbsp;</td>
                        <td style="width:69%;color: orangered">Welcome<br/></td>
                        <td style="width:3%">&nbsp;</td>
                        <td style="width:22%"></td>
                    </tr>   
                </tbody>
            </table>
        </div>      
    </header>
      <br />
       <!-- parte central la pagina donde pintamos el mapa y la tabla con datos del API 
           de San Francisco Food Trucks -->
      <center>
          <div style="width:95%;">
       <div >             
           <asp:Label ID="Label1" runat="server" Font-Size="12px" Text="Select food track type:" ForeColor="#CC6600" Font-Names="Montserrat"></asp:Label> 
            &nbsp;&nbsp; 
            <select id="typeFood" onchange="filtroTipo();" style="font-family:Montserrat;font-size:11px">
            <option selected disabled>Make a selection</option>
            <option value="Truck">Truck</option>
            <option value="Push Cart">Push Cart</option>
                       
            </select><br />&nbsp;          
                 
            <label id="total" style="font-family:Montserrat;font-size:20px;font-weight:bold"/><br />
             <asp:Label ID="Label2" runat="server" Text="Total " Font-Bold="false" Font-Size="16px" ForeColor="#ff6666" Font-Names="Montserrat"></asp:Label>
        </div >
        <div id="map" style="width:68%; height: 500px; position: relative; overflow: hidden; float:left"></div>
        <div id="Info" style="width: 30%; height: 500px; position: relative;float:left;overflow:scroll;overflow-x:hidden;overflow-y:scroll;">
           
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>

      </div>
          <br />
      </center>
       <!-- Footer de pagina -->
    <div class="footer">
       <div class="content">
       <iframe id="result" name="CodePen" title="CodePen Preview" src="foodTruck.aspx" sandbox="allow-forms allow-modals allow-pointer-lock allow-popups allow-same-origin allow-scripts allow-top-navigation-by-user-activation allow-downloads allow-presentation" allow="accelerometer; camera; encrypted-media; display-capture; geolocation; gyroscope; microphone; midi; clipboard-read; clipboard-write; web-share" scrolling="auto" allowTransparency="true" allowpaymentrequest="true" allowfullscreen="true" class="result-iframe " loading="lazy"></iframe>
       </div>        
     </div> 
    
  </body>
</html>

