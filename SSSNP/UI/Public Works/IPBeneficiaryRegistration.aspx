<%@ Page Title=":: :: SNSOP : : Beneficiary Enrollment " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="IPBeneficiaryRegistration" CodeBehind="IPBeneficiaryRegistration.aspx.cs" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script>
        $(document).ready(function () {

            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);

            $('#txtStartDate').datetimepicker();
        });
    </script>
    <script type="text/javascript" language="javascript">

        function OpenPopup() {

            window.open("Lookups/BeneficiaryLookup1.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

            return false;
        }

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
        }


    </script>
    <script type="text/javascript">
        function inputOnlyNumbers(evt) {
            var e = window.event || evt; // for trans-browser compatibility  
            var charCode = e.which || e.keyCode;
            if ((charCode > 45 && charCode < 58) || charCode == 8) {
                return true;
            }
            return false;
        }


        function AlphaNumericOnly(evt) {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue >= 48 && AsciiValue <= 57))
                event.returnValue = true;
            else {
                event.returnValue = false;

                alert("Enter Alphabets,  and Characters  Only");
            }
        }
    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                            </li>
                    <li class="">
                        <i class="fa fa-fw fa-desktop"></i>Implementing Partner
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Beneficiary</a>
                    </li>
                </ol>
            </div>

            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Beneficiary</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="115px">Search</asp:Label>
                                            <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                                OnClick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False"
                                                Font-Size="12px" Style="position: static" ToolTip="Rate Lookup" />
                                            &nbsp;
                                                <div>
                                                </div>
                                        </td>
                                        <td runat="server" id="trlookup" style="padding: 1px; margin: 1px" align="left">
                                            <a href="UploadsTrainee.aspx" class="btn btn-primary">Upload Beneficiary Details</a>
                                        </td>

                                    </tr>
                                    <tr>

                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="lblEnrollmentNo" Font-Bold="true" Visible="false" runat="server" CssClass="LBLBLACK" Width="365px"></asp:Label>
                                        </td>



                                        <td runat="server" id="Td1" style="padding: 1px; margin: 1px" align="left">
                                            <a href="UploadsTraineeUpdate.aspx" class="btn btn-primary">Upload Update Beneficiary Details</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left"></td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                            ControlToValidate="ddlIP" ErrorMessage="Please Select IP"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>



                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="160px">Name of household head</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtHeadName" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                            ControlToValidate="txtHeadName"
                                                            ErrorMessage="Please Enter Name of household head" SetFocusOnError="True"
                                                            ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="160px">Beneficiary Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlBeneficiary" AutoPostBack="True"
                                                            OnSelectedIndexChanged="BeneficiarySelect" runat="server"
                                                            class="form-control txtbox" Height="36px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Group">Group</asp:ListItem>
                                                            <asp:ListItem Value="CST">CST</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:HiddenField ID="hdBeneficiaryType" runat="server" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="ddlBeneficiary" ErrorMessage="Please Select Beneficiary Type?"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr runat="server" id="trGroup" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="160px">Group List</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlGroupList" runat="server" OnSelectedIndexChanged="ddlGroupList_SelectedIndexChanged"
                                                            class="form-control txtbox" Height="36px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                            <asp:ListItem Value="59">59</asp:ListItem>
                                                            <asp:ListItem Value="60">60</asp:ListItem>
                                                            <asp:ListItem Value="61">61</asp:ListItem>
                                                            <asp:ListItem Value="62">62</asp:ListItem>
                                                            <asp:ListItem Value="63">63</asp:ListItem>
                                                            <asp:ListItem Value="64">64</asp:ListItem>
                                                            <asp:ListItem Value="65">65</asp:ListItem>
                                                            <asp:ListItem Value="66">66</asp:ListItem>
                                                            <asp:ListItem Value="67">67</asp:ListItem>
                                                            <asp:ListItem Value="68">68</asp:ListItem>
                                                            <asp:ListItem Value="69">69</asp:ListItem>
                                                            <asp:ListItem Value="70">70</asp:ListItem>
                                                            <asp:ListItem Value="71">71</asp:ListItem>
                                                            <asp:ListItem Value="72">72</asp:ListItem>
                                                            <asp:ListItem Value="73">73</asp:ListItem>
                                                            <asp:ListItem Value="74">74</asp:ListItem>
                                                            <asp:ListItem Value="75">75</asp:ListItem>
                                                            <asp:ListItem Value="76">76</asp:ListItem>
                                                            <asp:ListItem Value="77">77</asp:ListItem>
                                                            <asp:ListItem Value="78">78</asp:ListItem>
                                                            <asp:ListItem Value="79">79</asp:ListItem>
                                                            <asp:ListItem Value="80">80</asp:ListItem>
                                                            <asp:ListItem Value="81">81</asp:ListItem>
                                                            <asp:ListItem Value="82">82</asp:ListItem>
                                                            <asp:ListItem Value="83">83</asp:ListItem>
                                                            <asp:ListItem Value="84">84</asp:ListItem>
                                                            <asp:ListItem Value="85">85</asp:ListItem>
                                                            <asp:ListItem Value="86">86</asp:ListItem>
                                                            <asp:ListItem Value="87">87</asp:ListItem>
                                                            <asp:ListItem Value="88">88</asp:ListItem>
                                                            <asp:ListItem Value="89">89</asp:ListItem>
                                                            <asp:ListItem Value="90">90</asp:ListItem>
                                                            <asp:ListItem Value="91">91</asp:ListItem>
                                                            <asp:ListItem Value="92">92</asp:ListItem>
                                                            <asp:ListItem Value="93">93</asp:ListItem>
                                                            <asp:ListItem Value="94">94</asp:ListItem>
                                                            <asp:ListItem Value="95">95</asp:ListItem>
                                                            <asp:ListItem Value="96">96</asp:ListItem>
                                                            <asp:ListItem Value="97">97</asp:ListItem>
                                                            <asp:ListItem Value="98">98</asp:ListItem>
                                                            <asp:ListItem Value="99">99</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                            <asp:ListItem Value="101">101</asp:ListItem>
                                                            <asp:ListItem Value="102">102</asp:ListItem>
                                                            <asp:ListItem Value="103">103</asp:ListItem>
                                                            <asp:ListItem Value="104">104</asp:ListItem>
                                                            <asp:ListItem Value="105">105</asp:ListItem>
                                                            <asp:ListItem Value="106">106</asp:ListItem>
                                                            <asp:ListItem Value="107">107</asp:ListItem>
                                                            <asp:ListItem Value="108">108</asp:ListItem>
                                                            <asp:ListItem Value="109">109</asp:ListItem>
                                                            <asp:ListItem Value="110">110</asp:ListItem>
                                                            <asp:ListItem Value="111">111</asp:ListItem>
                                                            <asp:ListItem Value="112">112</asp:ListItem>
                                                            <asp:ListItem Value="113">113</asp:ListItem>
                                                            <asp:ListItem Value="114">114</asp:ListItem>
                                                            <asp:ListItem Value="115">115</asp:ListItem>
                                                            <asp:ListItem Value="116">116</asp:ListItem>
                                                            <asp:ListItem Value="117">117</asp:ListItem>
                                                            <asp:ListItem Value="118">118</asp:ListItem>
                                                            <asp:ListItem Value="119">119</asp:ListItem>
                                                            <asp:ListItem Value="120">120</asp:ListItem>
                                                            <asp:ListItem Value="121">121</asp:ListItem>
                                                            <asp:ListItem Value="122">122</asp:ListItem>
                                                            <asp:ListItem Value="123">123</asp:ListItem>
                                                            <asp:ListItem Value="124">124</asp:ListItem>
                                                            <asp:ListItem Value="125">125</asp:ListItem>
                                                            <asp:ListItem Value="126">126</asp:ListItem>
                                                            <asp:ListItem Value="127">127</asp:ListItem>
                                                            <asp:ListItem Value="128">128</asp:ListItem>
                                                            <asp:ListItem Value="129">129</asp:ListItem>
                                                            <asp:ListItem Value="130">130</asp:ListItem>
                                                            <asp:ListItem Value="131">131</asp:ListItem>
                                                            <asp:ListItem Value="132">132</asp:ListItem>
                                                            <asp:ListItem Value="133">133</asp:ListItem>
                                                            <asp:ListItem Value="134">134</asp:ListItem>
                                                            <asp:ListItem Value="135">135</asp:ListItem>
                                                            <asp:ListItem Value="136">136</asp:ListItem>
                                                            <asp:ListItem Value="137">137</asp:ListItem>
                                                            <asp:ListItem Value="138">138</asp:ListItem>
                                                            <asp:ListItem Value="139">139</asp:ListItem>
                                                            <asp:ListItem Value="140">140</asp:ListItem>
                                                            <asp:ListItem Value="141">141</asp:ListItem>
                                                            <asp:ListItem Value="142">142</asp:ListItem>
                                                            <asp:ListItem Value="143">143</asp:ListItem>
                                                            <asp:ListItem Value="144">144</asp:ListItem>
                                                            <asp:ListItem Value="145">145</asp:ListItem>
                                                            <asp:ListItem Value="146">146</asp:ListItem>
                                                            <asp:ListItem Value="147">147</asp:ListItem>
                                                            <asp:ListItem Value="148">148</asp:ListItem>
                                                            <asp:ListItem Value="149">149</asp:ListItem>
                                                            <asp:ListItem Value="150">150</asp:ListItem>
                                                            <asp:ListItem Value="151">151</asp:ListItem>
                                                            <asp:ListItem Value="152">152</asp:ListItem>
                                                            <asp:ListItem Value="153">153</asp:ListItem>
                                                            <asp:ListItem Value="154">154</asp:ListItem>
                                                            <asp:ListItem Value="155">155</asp:ListItem>
                                                            <asp:ListItem Value="156">156</asp:ListItem>
                                                            <asp:ListItem Value="157">157</asp:ListItem>
                                                            <asp:ListItem Value="158">158</asp:ListItem>
                                                            <asp:ListItem Value="159">159</asp:ListItem>
                                                            <asp:ListItem Value="160">160</asp:ListItem>
                                                            <asp:ListItem Value="161">161</asp:ListItem>
                                                            <asp:ListItem Value="162">162</asp:ListItem>
                                                            <asp:ListItem Value="163">163</asp:ListItem>
                                                            <asp:ListItem Value="164">164</asp:ListItem>
                                                            <asp:ListItem Value="165">165</asp:ListItem>
                                                            <asp:ListItem Value="166">166</asp:ListItem>
                                                            <asp:ListItem Value="167">167</asp:ListItem>
                                                            <asp:ListItem Value="168">168</asp:ListItem>
                                                            <asp:ListItem Value="169">169</asp:ListItem>
                                                            <asp:ListItem Value="170">170</asp:ListItem>
                                                            <asp:ListItem Value="171">171</asp:ListItem>
                                                            <asp:ListItem Value="172">172</asp:ListItem>
                                                            <asp:ListItem Value="173">173</asp:ListItem>
                                                            <asp:ListItem Value="174">174</asp:ListItem>
                                                            <asp:ListItem Value="175">175</asp:ListItem>
                                                            <asp:ListItem Value="176">176</asp:ListItem>
                                                            <asp:ListItem Value="177">177</asp:ListItem>
                                                            <asp:ListItem Value="178">178</asp:ListItem>
                                                            <asp:ListItem Value="179">179</asp:ListItem>
                                                            <asp:ListItem Value="180">180</asp:ListItem>
                                                            <asp:ListItem Value="181">181</asp:ListItem>
                                                            <asp:ListItem Value="182">182</asp:ListItem>
                                                            <asp:ListItem Value="183">183</asp:ListItem>
                                                            <asp:ListItem Value="184">184</asp:ListItem>
                                                            <asp:ListItem Value="185">185</asp:ListItem>
                                                            <asp:ListItem Value="186">186</asp:ListItem>
                                                            <asp:ListItem Value="187">187</asp:ListItem>
                                                            <asp:ListItem Value="188">188</asp:ListItem>
                                                            <asp:ListItem Value="189">189</asp:ListItem>
                                                            <asp:ListItem Value="190">190</asp:ListItem>
                                                            <asp:ListItem Value="191">191</asp:ListItem>
                                                            <asp:ListItem Value="192">192</asp:ListItem>
                                                            <asp:ListItem Value="193">193</asp:ListItem>
                                                            <asp:ListItem Value="194">194</asp:ListItem>
                                                            <asp:ListItem Value="195">195</asp:ListItem>
                                                            <asp:ListItem Value="196">196</asp:ListItem>
                                                            <asp:ListItem Value="197">197</asp:ListItem>
                                                            <asp:ListItem Value="198">198</asp:ListItem>
                                                            <asp:ListItem Value="199">199</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="201">201</asp:ListItem>
                                                            <asp:ListItem Value="202">202</asp:ListItem>
                                                            <asp:ListItem Value="203">203</asp:ListItem>
                                                            <asp:ListItem Value="204">204</asp:ListItem>
                                                            <asp:ListItem Value="205">205</asp:ListItem>
                                                            <asp:ListItem Value="206">206</asp:ListItem>
                                                            <asp:ListItem Value="207">207</asp:ListItem>
                                                            <asp:ListItem Value="208">208</asp:ListItem>
                                                            <asp:ListItem Value="209">209</asp:ListItem>
                                                            <asp:ListItem Value="210">210</asp:ListItem>
                                                            <asp:ListItem Value="211">211</asp:ListItem>
                                                            <asp:ListItem Value="212">212</asp:ListItem>
                                                            <asp:ListItem Value="213">213</asp:ListItem>
                                                            <asp:ListItem Value="214">214</asp:ListItem>
                                                            <asp:ListItem Value="215">215</asp:ListItem>
                                                            <asp:ListItem Value="216">216</asp:ListItem>
                                                            <asp:ListItem Value="217">217</asp:ListItem>
                                                            <asp:ListItem Value="218">218</asp:ListItem>
                                                            <asp:ListItem Value="219">219</asp:ListItem>
                                                            <asp:ListItem Value="220">220</asp:ListItem>
                                                            <asp:ListItem Value="221">221</asp:ListItem>
                                                            <asp:ListItem Value="222">222</asp:ListItem>
                                                            <asp:ListItem Value="223">223</asp:ListItem>
                                                            <asp:ListItem Value="224">224</asp:ListItem>
                                                            <asp:ListItem Value="225">225</asp:ListItem>
                                                            <asp:ListItem Value="226">226</asp:ListItem>
                                                            <asp:ListItem Value="227">227</asp:ListItem>
                                                            <asp:ListItem Value="228">228</asp:ListItem>
                                                            <asp:ListItem Value="229">229</asp:ListItem>
                                                            <asp:ListItem Value="230">230</asp:ListItem>
                                                            <asp:ListItem Value="231">231</asp:ListItem>
                                                            <asp:ListItem Value="232">232</asp:ListItem>
                                                            <asp:ListItem Value="233">233</asp:ListItem>
                                                            <asp:ListItem Value="234">234</asp:ListItem>
                                                            <asp:ListItem Value="235">235</asp:ListItem>
                                                            <asp:ListItem Value="236">236</asp:ListItem>
                                                            <asp:ListItem Value="237">237</asp:ListItem>
                                                            <asp:ListItem Value="238">238</asp:ListItem>
                                                            <asp:ListItem Value="239">239</asp:ListItem>
                                                            <asp:ListItem Value="240">240</asp:ListItem>
                                                            <asp:ListItem Value="241">241</asp:ListItem>
                                                            <asp:ListItem Value="242">242</asp:ListItem>
                                                            <asp:ListItem Value="243">243</asp:ListItem>
                                                            <asp:ListItem Value="244">244</asp:ListItem>
                                                            <asp:ListItem Value="245">245</asp:ListItem>
                                                            <asp:ListItem Value="246">246</asp:ListItem>
                                                            <asp:ListItem Value="247">247</asp:ListItem>
                                                            <asp:ListItem Value="248">248</asp:ListItem>
                                                            <asp:ListItem Value="249">249</asp:ListItem>
                                                            <asp:ListItem Value="250">250</asp:ListItem>
                                                            <asp:ListItem Value="251">251</asp:ListItem>
                                                            <asp:ListItem Value="252">252</asp:ListItem>
                                                            <asp:ListItem Value="253">253</asp:ListItem>
                                                            <asp:ListItem Value="254">254</asp:ListItem>
                                                            <asp:ListItem Value="255">255</asp:ListItem>
                                                            <asp:ListItem Value="256">256</asp:ListItem>
                                                            <asp:ListItem Value="257">257</asp:ListItem>
                                                            <asp:ListItem Value="258">258</asp:ListItem>
                                                            <asp:ListItem Value="259">259</asp:ListItem>
                                                            <asp:ListItem Value="260">260</asp:ListItem>
                                                            <asp:ListItem Value="261">261</asp:ListItem>
                                                            <asp:ListItem Value="262">262</asp:ListItem>
                                                            <asp:ListItem Value="263">263</asp:ListItem>
                                                            <asp:ListItem Value="264">264</asp:ListItem>
                                                            <asp:ListItem Value="265">265</asp:ListItem>
                                                            <asp:ListItem Value="266">266</asp:ListItem>
                                                            <asp:ListItem Value="267">267</asp:ListItem>
                                                            <asp:ListItem Value="268">268</asp:ListItem>
                                                            <asp:ListItem Value="269">269</asp:ListItem>
                                                            <asp:ListItem Value="270">270</asp:ListItem>
                                                            <asp:ListItem Value="271">271</asp:ListItem>
                                                            <asp:ListItem Value="272">272</asp:ListItem>
                                                            <asp:ListItem Value="273">273</asp:ListItem>
                                                            <asp:ListItem Value="274">274</asp:ListItem>
                                                            <asp:ListItem Value="275">275</asp:ListItem>
                                                            <asp:ListItem Value="276">276</asp:ListItem>
                                                            <asp:ListItem Value="277">277</asp:ListItem>
                                                            <asp:ListItem Value="278">278</asp:ListItem>
                                                            <asp:ListItem Value="279">279</asp:ListItem>
                                                            <asp:ListItem Value="280">280</asp:ListItem>
                                                            <asp:ListItem Value="281">281</asp:ListItem>
                                                            <asp:ListItem Value="282">282</asp:ListItem>
                                                            <asp:ListItem Value="283">283</asp:ListItem>
                                                            <asp:ListItem Value="284">284</asp:ListItem>
                                                            <asp:ListItem Value="285">285</asp:ListItem>
                                                            <asp:ListItem Value="286">286</asp:ListItem>
                                                            <asp:ListItem Value="287">287</asp:ListItem>
                                                            <asp:ListItem Value="288">288</asp:ListItem>
                                                            <asp:ListItem Value="289">289</asp:ListItem>
                                                            <asp:ListItem Value="290">290</asp:ListItem>
                                                            <asp:ListItem Value="291">291</asp:ListItem>
                                                            <asp:ListItem Value="292">292</asp:ListItem>
                                                            <asp:ListItem Value="293">293</asp:ListItem>
                                                            <asp:ListItem Value="294">294</asp:ListItem>
                                                            <asp:ListItem Value="295">295</asp:ListItem>
                                                            <asp:ListItem Value="296">296</asp:ListItem>
                                                            <asp:ListItem Value="297">297</asp:ListItem>
                                                            <asp:ListItem Value="298">298</asp:ListItem>
                                                            <asp:ListItem Value="299">299</asp:ListItem>
                                                            <asp:ListItem Value="300">300</asp:ListItem>

                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdGroupId" runat="server" />
                                                        <asp:HiddenField ID="hdGroupNo" runat="server" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                            ControlToValidate="ddlGroupList" ErrorMessage="Please Select Group?" Visible="false"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>






                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label358" runat="server" CssClass="LBLBLACK" Width="165px">Household Size</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtFamilySize" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="2" TabIndex="1" onkeypress="NumberOnly()"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                            ControlToValidate="txtFamilySize" ErrorMessage="Please Enter Household size"
                                                            SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlSex" runat="server"
                                                            class="form-control txtbox" Height="36px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                                            ControlToValidate="ddlSex" ErrorMessage="Please Select Sex"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label405" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlAge" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label359" runat="server" CssClass="LBLBLACK" Width="137px">Name of spouse</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtSpouse" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                                            ControlToValidate="txtSpouse" ErrorMessage="Please Enter Name of spouse"
                                                            SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label360" runat="server" CssClass="LBLBLACK" Width="154px">No.of other adult household members </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNoofAdults" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:TextBox ID="txtNoofAdults" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="2" TabIndex="1" onkeypress="NumberOnly()" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                        
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                                            ControlToValidate="ddlNoofAdults"
                                                            ErrorMessage="Please Select No.of other adult household members"
                                                            SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Width="165px">Number of male adults</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberMaleAdults" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberMaleAdults" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="165px">Number of Female adults</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberFemaleAdults" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberFemaleAdults" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="165px">Number of adults Age above 65</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberAdultsAgeAbove65" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:TextBox ID="txtNumberAdultsAgeAbove65" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="165px">Number of adults age between 45 - 64</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberAdultsAgeBetween45_64" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:TextBox ID="txtNumberAdultsAgeBetween45_64" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Width="165px">Number of adults age between 18 - 30</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberAdultsAgeBetween18_30" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberAdultsAgeBetween18_30" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="165px">Number of adults age between 30 - 45 years</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberAdultsAgeBetween30_45" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberAdultsAgeBetween30_45" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>


                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label371" runat="server" CssClass="LBLBLACK" Width="165px">Number of dependents</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddldependents" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtdependents" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="2" TabIndex="1" onkeypress="NumberOnly()" oncopy="return false" onpaste="return false" oncut="return false" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                                            ControlToValidate="ddldependents"
                                                            ErrorMessage="Please Select Number of dependents" SetFocusOnError="True"
                                                            ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label11" runat="server" CssClass="LBLBLACK" Width="165px">Number of Female Children</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberFemaleChildren" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberFemaleChildren" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label13" runat="server" CssClass="LBLBLACK" Width="165px">Number of Male Children</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberMaleChildren" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberMaleChildren" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label14" runat="server" CssClass="LBLBLACK" Width="165px">Number of children age <5 years</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberChildrenAgeAbove5years" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberChildrenAgeAbove5years" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label15" runat="server" CssClass="LBLBLACK" Width="165px">Number of chilren age <12 years</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberChildrenAgeAbove12years" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberChildrenAgeAbove12years" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label16" runat="server" CssClass="LBLBLACK" Width="165px">Number of children age <18 years</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberChildrenAgeAbove18years" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberChildrenAgeAbove18years" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>





                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label25" runat="server" CssClass="LBLBLACK" Width="165px">Component</asp:Label>
                                                    </td>

                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <%--<asp:TextBox ID="txtSelectionCriteria" runat="server" class="form-control txtbox" 
                                                            Height="34px" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                        <asp:DropDownList ID="ddlComponent"
                                                            runat="server" class="form-control txtbox" Height="36px">
                                                            <asp:ListItem Value="Select">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Component 1 (Public Works)">Component 1 (LIPW)</asp:ListItem>
                                                            <asp:ListItem Value="Component 2 (Direct Income Support)">Component 2 (DIS)</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                </tr>
                                                <tr runat="server" id="trComponentHistory">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label30" runat="server" CssClass="LBLBLACK" Width="165px">Component History</asp:Label>
                                                    </td>

                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:GridView ID="grdComponentHistory" runat="server" CellPadding="4"
                                                            CssClass="GRD" ForeColor="#333333" AutoGenerateColumns="false" PageSize="50"
                                                            Width="100%">
                                                            <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#FFFFFF" CssClass="GRDITEM" HorizontalAlign="Left"
                                                                VerticalAlign="Middle" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Component" HeaderText="Component" />
                                                                <asp:TemplateField HeaderText="From" >
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Eval("Start_date", "{0:d}") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="To" >
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Eval("End_date", "{0:d}") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                           <%--     <asp:BoundField DataField="Start_date" HeaderText="From" />
                                                                <asp:BoundField DataField="End_date" HeaderText="To" />--%>
                                                            </Columns>
                                                            <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                            <EmptyDataTemplate>
                                                                No Request found
                                                            </EmptyDataTemplate>
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <EditRowStyle BackColor="#013161" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label27" runat="server" CssClass="LBLBLACK" Width="165px">Selection Reason</asp:Label>
                                                    </td>

                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtSelectionReason" runat="server" class="form-control txtbox"
                                                            Height="34px" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label372" runat="server" CssClass="LBLBLACK" Width="165px">Average Household Income</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtIncome" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label406" runat="server" CssClass="LBLBLACK" Width="165px">Source of Income</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtSourceIncome" runat="server" class="form-control txtbox"
                                                            Height="34px" MaxLength="80" TabIndex="1" TextMode="MultiLine"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                            ControlToValidate="ddlState" ErrorMessage="Please Select State"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>



                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label17" runat="server" CssClass="LBLBLACK" Width="165px">National ID Number</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtNationalID" runat="server" class="form-control txtbox"
                                                            Height="34px" MaxLength="20" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="rvtxtNationalID" runat="server" 
                                                            ControlToValidate="txtNationalID" ErrorMessage="Please Enter National ID" 
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label373" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlCounties" runat="server"
                                                            class="form-control txtbox" Height="33px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                            ControlToValidate="ddlCounties" ErrorMessage="Please Select County"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label404" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlPayam" runat="server"
                                                            class="form-control txtbox" Height="36px" Width="180px"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                   
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                            ControlToValidate="ddlPayam" ErrorMessage="Please Select Payam"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" OnSelectedIndexChanged="ddlBoma_SelectedIndexChanged"
                                                            class="form-control txtbox" AutoPostBack="true" Height="36px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:HiddenField ID="hdBoma" runat="server" />
                                                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                            ControlToValidate="ddlBoma" ErrorMessage="Please Select Boma"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label12" Visible="false" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;"></td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtEthnicityTribeRgin" Visible="false" runat="server" class="form-control txtbox"
                                                            Height="28px" onkeypress="return AlphaNumericOnly(event);" MaxLength="13" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label352" runat="server" CssClass="LBLBLACK" Width="137px">Address</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtAddress" runat="server" class="form-control txtbox"
                                                            Height="42px" MaxLength="100" TabIndex="1"
                                                            TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                                                            ControlToValidate="txtAddress" ErrorMessage="Please Enter Address"
                                                            SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label298" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtContactNo" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                                                            ControlToValidate="txtContactNo" ErrorMessage="Please Enter Contact No"
                                                            SetFocusOnError="True" ValidationGroup="group" Visible="False"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">Email</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control txtbox"
                                                            Height="34px" MaxLength="50" TabIndex="1"
                                                            ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ControlToValidate="txtEmail" ErrorMessage="Please Enter Valid Email"
                                                            SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="group">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label374" runat="server" CssClass="LBLBLACK" Width="143px">Second Line / Home</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtHomePhone" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                                                            ControlToValidate="txtHomePhone" ErrorMessage="Please Enter Second Line / Home"
                                                            SetFocusOnError="True" ValidationGroup="group" Visible="False"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label403" runat="server" CssClass="LBLBLACK" Width="143px">Legal Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlLegalStatus" runat="server"
                                                            class="form-control txtbox" Height="36px"
                                                            Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Refugee</asp:ListItem>
                                                            <asp:ListItem>Returnee</asp:ListItem>
                                                            <asp:ListItem>IDP</asp:ListItem>

                                                            <asp:ListItem>Host Community</asp:ListItem>
                                                            <asp:ListItem>Resident</asp:ListItem>
                                                            <asp:ListItem>Citizen</asp:ListItem>

                                                        </asp:DropDownList>
                                                    
                                                        <asp:RequiredFieldValidator ID="rvddlLegalStatus" runat="server"
                                                            ControlToValidate="ddlLegalStatus" ErrorMessage="Please Select Legal Status"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label402" runat="server" CssClass="LBLBLACK" Width="165px">Is Vulnerability?</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlIsdisability" runat="server"
                                                            class="form-control txtbox" Height="36px" Width="180px"
                                                            AutoPostBack="True"
                                                            OnSelectedIndexChanged="DropDownList25_SelectedIndexChanged">
                                                            <asp:ListItem Value="N">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                            <asp:ListItem Value="N">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                   
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server"
                                                            ControlToValidate="ddlIsdisability" ErrorMessage="Please Select Is Disability?"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false" id="trdisability">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label367" runat="server" CssClass="LBLBLACK" Width="165px">Vulnerability Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlDisability" runat="server"
                                                            class="form-control txtbox" Height="36px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                                                            ControlToValidate="ddlDisability" ErrorMessage="Please select Disability"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="165px">Biometric No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtBiometric" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="10" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label376" runat="server" CssClass="LBLBLACK" Width="165px">Beneficiary Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; height: 36px">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control txtbox"
                                                            Height="36px" onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image1" runat="server" Height="153px"
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            <asp:Label ID="Label407" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                Width="244px">First Alternate Payee Details</asp:Label>
                                        </td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top" align="left">
                                            <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                Width="244px">Second Alternate Payee Details</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtName1stAlternatePayee" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server"
                                                            ControlToValidate="txtName1stAlternatePayee" ErrorMessage="Please Enter First Alternate Payee"
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label18" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA1Gender" runat="server"
                                                            class="form-control txtbox" Width="180px" Height="36px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label19" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA1Age" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>


                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label28" runat="server" CssClass="LBLBLACK" Width="128px">National ID</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA1NationalID" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label22" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA1ContactNo" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>


                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label24" runat="server" CssClass="LBLBLACK" Width="165px">First Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; height: 36px">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" class="form-control txtbox"
                                                            Height="36px" onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName1" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image2" runat="server" Height="153px"
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>







                                            </table>
                                        </td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top" align="left">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtName2ndAlternatePayee" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                            ControlToValidate="txtName2ndAlternatePayee" ErrorMessage="Please Enter Second Alternate Payee"
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label20" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA2Gender" runat="server"
                                                            class="form-control txtbox" Width="180px" Height="36px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label21" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA2Age" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label29" runat="server" CssClass="LBLBLACK" Width="128px">National ID</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA2NationalID" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label23" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA2ContactNo" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>



                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label26" runat="server" CssClass="LBLBLACK" Width="165px">Second Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; height: 36px">
                                                        <asp:FileUpload ID="FileUpload3" runat="server" class="form-control txtbox"
                                                            Height="36px" onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName2" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image3" runat="server" Height="153px"
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>








                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                Width="165px">Bank Account Details</asp:Label>
                                        </td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label395" runat="server" CssClass="LBLBLACK" Width="165px">Name 
                                                        of Bank</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtBankName" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label396" runat="server" CssClass="LBLBLACK" Width="124px">Account 
                                                        No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtAccNo" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="15" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="124px">Retype 
                                                        Account No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtReAccNo" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="15" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                            ControlToCompare="txtAccNo" ControlToValidate="txtReAccNo"
                                                            ErrorMessage="Account No and Retype Account No should match"
                                                            SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">&nbsp;</td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px 5px 5px 45px">
                                            <asp:Button ID="btnSaveCustom" runat="server" CssClass="btn btn-primary"
                                                Height="32px" Visible="false" TabIndex="10" Text="Update"
                                                Width="90px" OnClick="btnSaveCustom_Click" />
                                            &nbsp;
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Save"
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnDelete" runat="server" CausesValidation="False"
                                                CssClass="btn btn-warning" Height="32px" OnClick="BtnClear0_Click"
                                                OnClientClick="return confirm('Do you want to delete the record ? ');"
                                                TabIndex="10" Text="Delete" Visible="false" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False"
                                                CssClass="btn btn-danger" Height="32px" OnClick="BtnClear_Click" TabIndex="10"
                                                Text="ClearAll" ToolTip="To Clear  the Screen" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <div id="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert"
                                                    href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label
                                                        ID="lblmsg" runat="server"></asp:Label>
                                            </div>

                                            <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
                                            <div style="color: Red">
                                                <asp:Label ID="lblresult" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile2" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile3" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
                <ProgressTemplate>
                    <div class="overlay">
                        <%--<div style=" z-index: 1000; margin-left: 350px;margin-top:200px;opacity: 1;-moz-opacity: 1;">--%>
                        <div style="z-index: 1000; margin-left: -210px; margin-top: 100px; opacity: 1; -moz-opacity: 1;">
                            <img alt="" src="../../Resource/Images/Processing.gif" />

                        </div>

                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>



        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

