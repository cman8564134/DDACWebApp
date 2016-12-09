<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="DDACWebApp.cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>CHECKOUT</title>
<link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="js/jquery.min.js"></script>
<script src="js/cart.js"></script>
    <script>
    function myFunction() {

        document.getElementById('form1').style.display = "none";
        myVar = setTimeout(showPage, 2000);
}
    function showPage() {
        document.getElementById('loader').style.display = "none";
        document.getElementById('form1').style.display = "block";
    }</script>
<!-- Custom Theme files -->
<!--theme-style-->
<link href="css/style.css" rel="stylesheet" type="text/css" media="all" />	
<!--//theme-style-->
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<!--fonts-->
<link href='http://fonts.googleapis.com/css?family=Lato:100,300,400,700,900' rel='stylesheet' type='text/css'>
<link href='http://fonts.googleapis.com/css?family=Roboto:400,100,300,500,700,900' rel='stylesheet' type='text/css'><!--//fonts-->
<!-- start menu -->
<link href="css/memenu.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="js/memenu.js"></script>
<script>$(document).ready(function(){$(".memenu").memenu();});</script>
<script type="text/javascript"src="js/simpleCart.min.js"> </script>

</head>
<body onload="myFunction()" style="margin:0;">
    <div id="loader"></div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerMain"
            runat="server"
            EnablePageMethods="true" 
            ScriptMode="Release">
            </asp:ScriptManager>
<div class="header">
	<div class="header-top">
		<div class="container">
			<div class="header-left">
					<div class="cart box_1">
						<a href="login.aspx"onclick="loginalert()">
						<h3> <div class="total">
							<span class="simpleCart_total"></span> (<span id="simpleCart_quantity" class="simpleCart_quantity"></span> items)</div>
							<img src="images/cart.png" alt=""/></h3>
						</a>
						<p><a href="javascript:;" class="simpleCart_empty">Empty Cart</a></p>

					</div>
					<div class="clearfix"> </div>
			</div>
				<div class="clearfix"> </div>
		</div>
		</div>
		<div class="container">
			<div class="head-top">
				<div class="logo">
					<a href="index.html"><img src="images/logo.png" alt="" height="50" width="200"></a>	
				</div>
		  <div class=" h_menu4">
				<ul class="memenu skyblue">
					  <li class="active grid"><a class="color8" href="index.html">Home</a></li>	
				      <li><a class="color1" href="#">Cruises</a>
				      	<div class="mepanel">
						<div class="row">
							<div class="col1">
								<div class="h_nav">
									<ul>
										<li><a href="SilverCruise.html">Silver Cruise</a></li>
										<li><a href="CarribeanCruise.html">Carribean Cruise</a></li>
									</ul>	
								</div>							
							</div>
                            
							<div class="col1">
								<div class="h_nav">
									<ul>
										<li><a href="GoldenCruise.html">Golden Cruise</a></li>
									</ul>	
								</div>							
							</div>
                            

							<div class="col1">
								<div class="h_nav">
									<h4>Popular Cruise</h4>
									<ul>
										<li><a href="SilverCruise.html">Silver Cruise</a></li>
									</ul>	
								</div>												
							</div>
						  </div>
						</div>
					</li>
				    <li class="grid"><a class="color2" href="#">	Destination</a>
					  	<div class="mepanel">
						<div class="row">
							<div class="col1">
								<div class="h_nav">
									<ul>
										<li><a href="SilverCruise.html">Singapore</a></li>
										<li><a href="CarribeanCruise.html">Rio de Janeiro</a></li>
									</ul>	
								</div>							
							</div>
                            
							<div class="col1">
								<div class="h_nav">
									<ul>
										<li><a href="GoldenCruise.html">Egypt</a></li>
									</ul>	
								</div>							
							</div>
                            
							<div class="col1">
								<div class="h_nav">
									<h4>Popular Destination</h4>
									<ul>
										<li><a href="SilverCruise.html">Singapore</a></li>
									</ul>	
								</div>												
							</div>
						  </div>
						</div>
			    
				<li><a class="color4" href="blog.html">Announcements</a></li>				
				<li><a class="color6" href="contact.html">Contact</a></li>
			  </ul> 
			</div>
				
				<div class="clearfix"> </div>
		</div>
		</div>

	</div>

	
<div class="container">
	<div class="check">	 
			 <h1>My Bookings </h1>

                     
					<table>
                        <tr>
                        <div class="simpleCart_items" style="padding-left:50px"></div>
                        </tr>
                        <tr></tr>
                        <tr>
    <td>   </td>
    <td>   </td>
    <td>   </td>
    <td><h5 style="padding-left:450px">Subtotal</h5></td>
    <td><div style="padding-left:10px"class="simpleCart_total" ></div></td>
</tr>
<tr>
    <td>   </td>
    <td>   </td>
    <td>   </td>
    <td><h5 style="padding-left:450px">Estimated shipping</h5></td>
    <td><div style="padding-left:10px"class="simpleCart_shipping"></div></td>
</tr>
<tr>
    <td>   </td>
    <td>   </td>
    <td>   </td>
    <td><h3 style="padding-left:450px">Total</h3></td>
    <td><div style="padding-left:10px"class="simpleCart_grandTotal"></div></td>
</tr>
                        <tr><td><i>Please ensure that the information here is correct: </i> </td></tr>
<tr>
    <asp:ListView ID="ListView1" runat="server">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>First Name:</td>
                            <td><asp:Label ID="value1" runat="server" Text=' <%# Bind("first_name") %> ' /><br />
                            </td>
                        </tr>
                        <tr>
                            <td>Last Name:</td>
                            <td>
                                <asp:Label ID="value2" runat="server" Text=' <%# Bind("last_name") %> ' />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>Gender:</td>
                            <td>
                                <asp:Label ID="value3" runat="server" Text=' <%# Bind("gender") %> ' />
                                
                                <br />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:ListView>
</tr>
<tr>
    <td>   </td>
    <td>   </td>
    <td>   </td>
    <td>   </td>
    <td><a  href="javascript:;" class="simpleCart_checkout" onclick="saveToDatabase()">Checkout</a></td>
</tr>
					</table>
                     	
		  
		
			<div class="clearfix"> </div>
	 </div>
	 </div>


<!--//content-->
<div class="footer">
				<div class="container">
			<div class="footer-top-at">
			
				<div class="col-md-4 amet-sed">
				<h4>MORE INFO</h4>
				<ul class="nav-bottom">
						<li><a href="contact.html">Contact Us</a></li>
					</ul>	
				</div>
				<div class="col-md-4 amet-sed ">
				<h4>CONTACT US</h4>
				
					<p>
Princess Cruise</p>
					<p>Standard Office Hours (9AM-5PM) Asia Pacific Time Region UTC+08:00 </p>
					<p>office:  +12 34 995 0792</p>
					<ul class="social">
						<li><a href="https://www.facebook.com/PrincessCruises"><i> </i></a></li>						
						<li><a href="https://twitter.com/PrincessCruises"><i class="twitter"> </i></a></li>
						<li><a href="http://www.princess.com/news/"><i class="rss"> </i></a></li>
						<li><a href="#"><i class="gmail"> </i></a></li>
						
					</ul>
				</div>
				<div class="col-md-4 amet-sed">
					<h4>Newsletter</h4>
					<p>Sign Up to get all news update
and promo</p>
					<form>
						<input type="text" value="" onfocus="this.value='';" onblur="if (this.value == '') {this.value ='';}"/>
						<input type="submit" value="Sign up"/>
					</form>
				</div>
				<div class="clearfix"> </div>
			</div>
		</div>
		<div class="footer-class">
		<p >© 2016 Princess Cruise All Rights Reserved </p>
		</div>
		</div>
    </form>
</body>
</html>
