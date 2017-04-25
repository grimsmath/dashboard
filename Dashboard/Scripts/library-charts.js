var LibraryCharts = {
    version: "1.0",
    author: "David King, Jr.",

    patronCountByYear: function (container, api) {
        $.ajax({
            url: api,
            dataType: "json",
            success: function (jsonData) {
                var seriesData = [];
                var myCategories = [];

                for (var i = 0; i < jsonData.model.length; i++) {
                    var x = jsonData.model[i].ServerYear;
                    var y = jsonData.model[i].PatronCount;

                    myCategories.push(x);
                    seriesData.push([x, y]);
                }

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: container,
                        defaultSeriesType: 'bar'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: 'Patron Count By Year'
                    },
                    xAxis: {
                        categories: myCategories
                    },
                    yAxis: {
                        title: {
                            text: "Patron Count"
                        }
                    },
                    series: [{
                        name: "# of Patrons",
                        data: seriesData,
                        showInLegend: false,
                        color: '#519BBC',
                        dataLabels: {
                            enabled: true,
                            rotation: 0,
                            color: '#444444',
                            align: 'center',
                            x: -50,
                            y: 0,
                            formatter: function () {
                                return LibraryUtil.addCommas(this.y);
                            },
                            style: {
                                font: 'bold 11px Verdana, sans-serif'
                            }
                        },
                    }]
                });
            }
        });
    },

    patronCountLast7: function (container, api) {
        $.ajax({
            url: api,
            dataType: "json",
            success: function (jsonData) {
                var seriesData = [];
                var myCategories = [];

                for (var i = 0; i < jsonData.model.length; i++) {

                    var myDay = jsonData.model[i].ServerDay;

                    var x = myDay.Month + "/" + myDay.Day;
                    var y = jsonData.model[i].PatronCount;

                    myCategories.push(x);
                    seriesData.push([x, y]);
                }

                var graphType = (seriesData.length > 1) ? 'area' : 'column';

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: container,
                        defaultSeriesType: graphType
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: 'Patron Count (Current Week)'
                    },
                    xAxis: {
                        categories: myCategories
                    },
                    yAxis: {
                        title: {
                            text: "Patron Count"
                        }
                    },
                    series: [{
                        name: "# of Patrons",
                        data: seriesData,
                        showInLegend: false,
                        dataLabels: {
                            enabled: true,
                            rotation: -90,
                            color: '#444444',
                            align: 'center',
                            x: 5,
                            y: 50,
                            formatter: function () {
                                return LibraryUtil.addCommas(this.y);
                            },
                            style: {
                                font: 'bold 11px Verdana, sans-serif'
                            }
                        },
                    }]
                });
            }
        });
    },

    patronCountByDay: function (container, api) {
        $.ajax({
            url: api,
            dataType: "json",
            success: function (jsonData) {
                var seriesData = [];
                var myCategories = [];

                for (var i = 0; i < jsonData.model.length; i++) {
                    var x = jsonData.model[i].ServerDate;
                    var y = jsonData.model[i].PatronCount;

                    myCategories.push(x);
                    seriesData.push([x, y]);
                }

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: container,
                        defaultSeriesType: 'column'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: 'Weekly Patron Count'
                    },
                    xAxis: {
                        categories: myCategories
                    },
                    yAxis: {
                        title: {
                            text: "Computer Logins"
                        }
                    },
                    series: [{
                        name: "# of Logins",
                        data: seriesData,
                        showInLegend: false,
                        dataLabels: {
                            enabled: true,
                            rotation: -90,
                            color: '#444444',
                            align: 'center',
                            x: 5,
                            y: 0,
                            formatter: function () {
                                return LibraryUtil.addCommas(this.y);
                            },
                            style: {
                                font: 'bold 11px Verdana, sans-serif'
                            }
                        },
                    }]
                });
            }
        });
    },

    loginCountByYear: function (container, api) {
        var chartLabUsageByYear;

        $.ajax({
            url: api,
            dataType: "json",
            success: function (jsonData) {
                var seriesData = [];
                var myCategories = [];

                for (var i = 0; i < jsonData.data.length; i++) {
                    var x = jsonData.data[i].LabYear;
                    var y = jsonData.data[i].LoginCount;

                    myCategories.push(x);
                    seriesData.push([x, y]);
                }

                var chartLabUsageByYear = new Highcharts.Chart({
                    chart: {
                        renderTo: container,
                        defaultSeriesType: 'column'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: 'Computer Logins By Year'
                    },
                    xAxis: {
                        categories: myCategories
                    },
                    yAxis: {
                        title: {
                            text: "Computer Logins"
                        }
                    },
                    series: [{
                        name: "# of Logins",
                        data: seriesData,
                        showInLegend: false,
                        color: '#0ACF00',
                        dataLabels: {
                            enabled: true,
                            rotation: -90,
                            color: '#444444',
                            align: 'center',
                            x: 5,
                            y: -30,
                            formatter: function () {
                                return LibraryUtil.addCommas(this.y);
                            },
                            style: {
                                font: 'bold 11px Verdana, sans-serif'
                            }
                        },
                    }]
                });
            }
        });
    },

    dashboardPatronCount: function (container, api) {
        $.ajax({
            url: api,
            dataType: "json",
            success: function (jsonData) {
                var seriesData1 = [];
                var seriesData2 = [];
                var myCategories = [];

                var data1 = jsonData.series1.Data.model;
                var data2 = jsonData.series2.Data.model;
                console.log(data1);
                console.log(data2);

                for (var i = 0; i < data1.length; i++) {
                    var myDay = data1[i];

                    var x = myDay.ServerMonth + "/" + myDay.ServerDay;
                    var y1 = data1[i].LoginCount;
                    var y2 = data2[i].LoginCount;

                    myCategories.push(x);
                    seriesData1.push([x, y1]);
                    seriesData2.push([x, y2]);
                }

                var graphType = (seriesData1.length > 1) ? 'line' : 'column';

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: container,
                        defaultSeriesType: graphType
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: 'Computer Logins (Current Week)'
                    },
                    xAxis: {
                        categories: myCategories
                    },
                    yAxis: {
                        title: {
                            text: "Computer Logins"
                        }
                    },
                    series: [{
                        name: "Current Week & Year",
                        data: seriesData1,
                        showInLegend: true,
                        color: '#FF8B00',
                        dataLabels: {
                            enabled: false,
                            color: '#444444',
                            align: 'center',
                            formatter: function () {
                                return LibraryUtil.addCommas(this.y);
                            },
                            style: {
                                font: 'bold 11px Verdana, sans-serif'
                            }
                        }
                    },
                    {
                        name: "Same Week Last Year",
                        data: seriesData2,
                        showInLegend: true,
                        graphType: 'bar',
                        color: '#99ABD1',
                        dataLabels: {
                            enabled: false,
                            rotation: -90,
                            color: '#99ABD1',
                            align: 'center',
                            x: 5,
                            y: 50,
                            formatter: function () {
                                return LibraryUtil.addCommas(this.y);
                            },
                            style: {
                                font: 'bold 11px Verdana, sans-serif'
                            }
                        }
                    }]
                });
            }
        });
    },

    top10fulltextdownloads: function (container, api) {
        $.ajax({
            url: api,
            dataType: "json",
            success: function (jsonData) {
                var seriesData = [];
                var myCategories = [];

                for (var i = 0; i < jsonData.model.length; i++) {

                    var entry = jsonData.model[i];

                    var x = entry.Vendor + ": " + entry.Journal;
                    var y = entry.Total;

                    myCategories.push(x);
                    seriesData.push([x, y]);
                }

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: container,
                        defaultSeriesType: 'pie'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: 'Top 10 Full-Text Downloads'
                    },
                    xAxis: {
                        categories: myCategories
                    },
                    yAxis: {
                        title: {
                            text: "Full Text Downloads"
                        }
                    },
                    series: [{
                        name: "# of Downloads",
                        data: seriesData,
                        showInLegend: false,
                        color: '#FF8B00',
                        allowPointSelect: true,
                        cursor: 'pointer',
                        showInLegend: true,
                        dataLabels: {
                            enabled: false,
                            color: '#444444',
                            align: 'center',
                            x: 0,
                            y: 0,
                            formatter: function () {
                                return LibraryUtil.addCommas(this.point.y);
                            },
                            style: {
                                font: 'bold 11px Verdana, sans-serif'
                            }
                        },
                    }]
                });
            }
        });
    },

    top10journalsearches: function (container, api) {
        $.ajax({
            url: api,
            dataType: "json",
            success: function (jsonData) {
                var seriesData = [];
                var myCategories = [];

                for (var i = 0; i < jsonData.model.length; i++) {

                    var entry = jsonData.model[i];

                    var x = entry.JournalVendor + ": " + entry.JournalDB;
                    var y = entry.Total;

                    myCategories.push(x);
                    seriesData.push([x, y]);
                }

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: container,
                        defaultSeriesType: 'pie'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: 'Top 10 Searched Databases'
                    },
                    xAxis: {
                        categories: myCategories
                    },
                    yAxis: {
                        title: {
                            text: "Searched Databases"
                        }
                    },
                    legend: {
                        layout: "vertical"
                    },
                    series: [{
                        name: "# of Searches",
                        data: seriesData,
                        showInLegend: true,
                        color: '#FF8B00',
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: false,
                            color: '#444444',
                            align: 'center',
                            x: 0,
                            y: 0,
                            formatter: function () {
                                return LibraryUtil.addCommas(this.point.y);
                            },
                            style: {
                                font: 'bold 11px Verdana, sans-serif'
                            }
                        },
                    }]
                });
            }
        });
    },

    tickler1: function (container, api, year) {
        $.ajax({
            url: api + year,
            dataType: "json",
            success: function (jsonData) {
                var seriesData = [];
                var myCategories = [];

                for (var i = 0; i < jsonData.model.length; i++) {

                    var entry = jsonData.model[i];

                    var x = entry.Cataloger;
                    var y = entry.TitleCount;

                    myCategories.push(x);
                    seriesData.push([x, y]);
                }

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: container,
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false
                    },
                    title: {
                        text: 'Individual Cataloging By Title For ' + year
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            showInLegend: true,
                            dataLabels: {
                                enabled: true,
                                color: '#000000',
                                connectorColor: '#000000',
                                formatter: function () {
                                    return '<b>' + this.point.name + '</b>: ' + this.point.y;
                                }
                            }
                        }
                    },
                    series: [{
                        type: 'pie',
                        name: 'Titles',
                        data: seriesData
                    }]
                });
            }
        });
    },

    tickler2: function (container, api, year) {
        $.ajax({
            url: api + year,
            dataType: "json",
            success: function (jsonData) {
                var seriesData = [];
                var myCategories = [];

                for (var i = 0; i < jsonData.model.length; i++) {

                    var entry = jsonData.model[i];

                    var x = entry.Cataloger;
                    var y = entry.VolumeCount;

                    myCategories.push(x);
                    seriesData.push([x, y]);
                }

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: container,
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false
                    },
                    title: {
                        text: 'Individual Cataloging By Volume For ' + year
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            showInLegend: true,
                            dataLabels: {
                                enabled: true,
                                color: '#000000',
                                connectorColor: '#000000',
                                formatter: function () {
                                    return '<b>' + this.point.name + '</b>: ' + this.point.y;
                                }
                            }
                        }
                    },
                    series: [{
                        type: 'pie',
                        name: 'Titles',
                        data: seriesData
                    }]
                });
            }
        });
    },
};