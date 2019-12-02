$.ajax({
    type: "POST",
    url: "/Home/VisualizeActivityResult",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (chData) {
        var aData = chData;
        var aLabels = aData[0];
        var aDatasets1 = aData[1];

        var dataT = {
            labels: aLabels,
            datasets: [{
                label: "Antal stemmer",
                data: aDatasets1,
                backgroundColor: ["rgba(54, 162, 235, 0.2)", "rgba(255, 99, 132, 0.2)", "rgba(255, 159, 64, 0.2)", "rgba(255, 205, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(153, 102, 255, 0.2)", "rgba(201, 203, 207, 0.2)"]
            }]
        };
        var ctx = $("#activityResult").get(0).getContext("2d");
        var myNewChart = new Chart(ctx, {
            type: 'bar',
            data: dataT,
            options: {
                responsive: true,
                title: { display: true, text: 'Social aktivitet' },
                legend: { position: 'bottom' },
                scales: {
                    xAxes: [{ gridLines: { display: false }, display: true, scaleLabel: { display: false, labelString: 'Aktivitet' } }],
                    yAxes: [{ gridLines: { display: false }, display: true, scaleLabel: { display: false, labelString: 'Antal stemmer' }, ticks: { stepSize: 1, beginAtZero: true, suggestedMax: Math.max(...aDatasets1) + 2} }]
                },
            }
        });
    }
});

$.ajax({
    type: "POST",
    url: "/Home/VisualizeFoodResult",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (chData) {
        var aData = chData;
        var aLabels = aData[0];
        var aDatasets1 = aData[1];

        var dataT = {
            labels: aLabels,
            datasets: [{
                label: "Antal stemmer",
                data: aDatasets1,
                backgroundColor: ["rgba(54, 162, 235, 0.2)", "rgba(255, 99, 132, 0.2)", "rgba(255, 159, 64, 0.2)", "rgba(255, 205, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(153, 102, 255, 0.2)"]
            }]
        };
        var ctx = $("#foodResult").get(0).getContext("2d");
        var myNewChart = new Chart(ctx, {
            type: 'bar',
            data: dataT,
            options: {
                responsive: true,
                title: { display: true, text: 'Fredagens ret' },
                legend: { position: 'bottom' },
                scales: {
                    xAxes: [{ gridLines: { display: false }, display: true, scaleLabel: { display: false, labelString: 'Fredagens ret' } }],
                    yAxes: [{ gridLines: { display: false }, display: true, scaleLabel: { display: false, labelString: 'Antal stemmer' }, ticks: { stepSize: 1, beginAtZero: true, suggestedMax: Math.max(...aDatasets1) + 2} }]
                },
            }
        });
    }
});


const categoryButton = document.querySelectorAll(".categoryButton")
categoryButton.forEach(element => {
    element.addEventListener("click", (event) => {
        event.preventDefault();
        event.target.nextElementSibling.classList.toggle("hide");
    })
});