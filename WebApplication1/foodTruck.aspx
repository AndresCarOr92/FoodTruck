 <!-- Reto Food Trucks, esto es el frond-end y aqui comienza -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="foodTruck.aspx.cs" Inherits="WebApplication1.foodTruck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
     <!--Head donde se encuentran los Metas, styles y script -->
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name='viewport' content='width=device-width, initial-scale=1'/>
    <!--Este Style contiene todos los CSS-->
  <link href="style.css" rel="stylesheet" />
    <title></title>
    <script>
  if (document.location.search.match(/type=embed/gi)) {
    window.parent.postMessage("resize", "*");
  }
</script>

</head>
<body>
     <!-- Esta vista pinta un camion de comida rapida utilizamos HTML y CSS  -->
<div id='truck'><%-- Esta division pinta la camioneta--%>
	<div class='inner'>
		<h1>HOTT SNACSS</h1>
		<div class='side'><span></span><span></span></div>
		<div class='side'><span></span><span></span></div>
		<div class='side'><span></span><span></span></div>
		<div class='side'><span></span><span></span></div>
		<div class='side'><span></span><span></span></div>
		<div class='side'><span></span><span></span></div>
		<div class="shadow"></div>
		<div class="counter"></div>
		<div class='menu'>
			<h2>Burgers</h2>
			<p>
				<span>Hamburger</span>
				<span>$3</span>
			</p>
			<p>
				<span>Cheeseburger</span>
				<span>$3.50</span>
			</p>
			<p>
				<span>Double Cheese Burger</span>
				<span>$4.50</span>
			</p>
			<p>
				<span>Veggie Burger</span>
				<span>$3</span>
			</p>
			<h2>Hot Dogs</h2>
			<p>
				<span>Hot Dog</span>
				<span>$3</span>
			</p>
			<p>
				<span>Chilli Dog</span>
				<span>$5</span>
			</p>
			<p>
				<span>Veggie Hot Dog</span>
				<span>$3</span>
			</p>
			<h2>Chili</h2>
			<p>
				<span>Cup</span>
				<span>$2</span>
			</p>
			<p>
				<span>Bowl</span>
				<span>$4</span>
			</p>
			<h2>Sides</h2>
			<p>
				<span>Chips</span>
				<span>$1</span>
			</p>
			<p>
				<span>Fries</span>
				<span>$2</span>
			</p>
			<p>
				<span>Sweet Potato Fries</span>
				<span>$3</span>
			</p>
			<p>
				<span>Onion Rings</span>
				<span>$3</span>
			</p>
			<h2>Drinks</h2>
			<p>
				<span>Soda</span>
				<span>$1</span>
			</p>
			<p>
				<span>Iced Tea</span>
				<span>$1</span>
			</p>
			<p>
				<span>Fresh Squeezed Lemonade</span>
				<span>$2</span>
			</p>
		</div>

	</div>
</div>
     <!-- Esta parte pinta una carretera con CSS y HTML -->
<svg style='display:none' version='1.1' xmlns='http://www.w3.org/2000/svg'>
	<defs>
		<filter id='squiggly'>
			<feturbulence basefrequency='0.05 0.025' id='turbulence' numoctaves='3' result='noise' seed='0'></feturbulence>
			<fedisplacementmap id='displacement' in2='noise' in='SourceGraphic' scale='2'></fedisplacementmap>
		</filter>
		<filter id='squiggly2'>
			<feturbulence basefrequency='0.04 0.015' id='turbulence' numoctaves='3' result='noise' seed='1'></feturbulence>
			<fedisplacementmap in2='noise' in='SourceGraphic' scale='3'></fedisplacementmap>
		</filter>
	</defs>
</svg>
</body>

</html>

