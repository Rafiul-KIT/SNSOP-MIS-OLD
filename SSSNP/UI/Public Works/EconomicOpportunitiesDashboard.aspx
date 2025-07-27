<%@ Page Title=":: :: SNSOP : : Economic Opportunities Dashboard" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="EconomicOpportunitiesDashboard.aspx.cs" Inherits="SSSNP.UI.Public_Works.EconomicOpportunitiesDashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
    function loadEnrollmentChart() {
        fetch('EconomicOpportunitiesDashboard.aspx/GetEnrollmentData', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({})
        })
        .then(response => response.json())
        .then(data => {
            const result = data.d;
            const labels = result.map(item => item.CountyName);
            const values = result.map(item => item.TotalEnrolled);

            const ctx = document.getElementById('enrollmentChart').getContext('2d');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Enrolled Beneficiaries',
                        data: values,
                        backgroundColor: 'rgba(54, 162, 235, 0.7)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1,
                        maxBarThickness: 60
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: { display: false }
                    },
                    scales: {
                        y: {
                            beginAtZero: true,
                            title: { display: true, text: 'Beneficiaries Count' }
                        }
                    }
                }
            });
        });
    }

    window.onload = loadEnrollmentChart;
    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left" style="vertical-align: top">
                <ol class="breadcrumb">
                    <li>You are here&nbsp;!&nbsp; &nbsp; &nbsp;</li>
                    <li>
                        <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                    </li>
                    <li class="">
                        <i class="fa fa-fw fa-desktop"></i>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Economic Opportunities Dashboard</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Economic Opportunities Dashboard</h3>
                            </div>
                            <div class ="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="card shadow p-3 mb-4 bg-white rounded">
                                            <h4 class="text-primary">County-wise Enrolled Beneficiaries</h4>
                                            <div style="height: 250px; max-width: 600px; margin: auto;">
                                                <canvas id="enrollmentChart" style="height: 100%; width: 100%;"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>   
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
