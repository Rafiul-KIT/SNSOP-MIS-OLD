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
                        label: 'Count: ',
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
                            beginAtZero: true
                        }
                    }
                }
            });
        });

        fetch('EconomicOpportunitiesDashboard.aspx/GetBeneficiaryAssignedGroupData', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({})
        })
            .then(response => response.json())
            .then(data => {
                const result = data.d;
                const labels = result.map(item => item.CountyName);
                const values = result.map(item => item.TotalEnrolled);

                const ctx = document.getElementById('beneficiariesAssignedAGroupChart').getContext('2d');
                new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Count: ',
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
                                beginAtZero: true
                            }
                        }
                    }
                });
            });

        fetch('EconomicOpportunitiesDashboard.aspx/GetFinlitData', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({})
        })
            .then(response => response.json())
            .then(data => {
                const result = data.d;
                const labels = result.map(item => item.CountyName);
                const values = result.map(item => item.TotalEnrolled);

                const ctx = document.getElementById('finLitChart').getContext('2d');
                new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Count: ',
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
                                beginAtZero: true
                            }
                        }
                    }
                });
            });

        fetch('EconomicOpportunitiesDashboard.aspx/GetLivelihoodTrainingData', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({})
        })
            .then(response => response.json())
            .then(data => {
                const result = data.d;
                const labels = result.map(item => item.CountyName);
                const values = result.map(item => item.TotalEnrolled);

                const ctx = document.getElementById('livelihoodTrainingChart').getContext('2d');
                new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Count: ',
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
                                beginAtZero: true
                            }
                        }
                    }
                });
            });

        fetch('EconomicOpportunitiesDashboard.aspx/GetLifeskillData', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({})
        })
            .then(response => response.json())
            .then(data => {
                const result = data.d;
                const labels = result.map(item => item.CountyName);
                const values = result.map(item => item.TotalEnrolled);

                const ctx = document.getElementById('lifeskillsTrainingChart').getContext('2d');
                new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Count: ',
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
                                beginAtZero: true
                            }
                        }
                    }
                });
            });

        fetch('EconomicOpportunitiesDashboard.aspx/GetSavingsData', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({})
        })
            .then(response => response.json())
            .then(data => {
                const result = data.d;
                const labels = result.map(item => item.CountyName);
                const values = result.map(item => item.TotalEnrolled);

                const ctx = document.getElementById('savingsChart').getContext('2d');
                new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Count: ',
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
                                beginAtZero: true
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
                                    <div class="col-md-6">
                                        <div class="card shadow p-3 mb-4 bg-white rounded">
                                            <h4 class="text-primary">Beneficiary Enrollment</h4>
                                            <div style="height: 200px; max-width: 600px; margin: auto;">
                                                <canvas id="enrollmentChart" style="height: 100%; width: 100%;"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card shadow p-3 mb-4 bg-white rounded">
                                            <h4 class="text-primary">Beneficiaries Assigned A Group</h4>
                                            <div style="height: 200px; max-width: 600px; margin: auto;">
                                                <canvas id="beneficiariesAssignedAGroupChart" style="height: 100%; width: 100%;"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card shadow p-3 mb-4 bg-white rounded">
                                            <h4 class="text-primary">Livelihood Training</h4>
                                            <div style="height: 200px; max-width: 600px; margin: auto;">
                                                <canvas id="livelihoodTrainingChart" style="height: 100%; width: 100%;"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card shadow p-3 mb-4 bg-white rounded">
                                            <h4 class="text-primary">Financial Literacy</h4>
                                            <div style="height: 200px; max-width: 600px; margin: auto;">
                                                <canvas id="finLitChart" style="height: 100%; width: 100%;"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card shadow p-3 mb-4 bg-white rounded">
                                            <h4 class="text-primary">Life-skills training</h4>
                                            <div style="height: 200px; max-width: 600px; margin: auto;">
                                                <canvas id="lifeskillsTrainingChart" style="height: 100%; width: 100%;"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card shadow p-3 mb-4 bg-white rounded">
                                            <h4 class="text-primary">Savings Group Attendance</h4>
                                            <div style="height: 200px; max-width: 600px; margin: auto;">
                                                <canvas id="savingsChart" style="height: 100%; width: 100%;"></canvas>
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
