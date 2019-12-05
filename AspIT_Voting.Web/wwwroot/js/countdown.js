
let d = new Date();

function getWeekNumber() {
    // Copy date so don't modify original
    d = new Date(Date.UTC(d.getFullYear(), d.getMonth(), d.getDate()));

    // Set to nearest Thursday: current date + 4 - current day number
    // Make Sunday's day number 7
    d.setUTCDate(d.getUTCDate() + 4 - (d.getUTCDay() || 7));

    // Get first day of year
    let yearStart = new Date(Date.UTC(d.getUTCFullYear(), 0, 1));

    // Calculate full weeks to nearest Thursday
    let weekNo = Math.ceil((((d - yearStart) / 86400000) + 1) / 7);

    // Return array of year and week number
    return [d.getUTCFullYear(), weekNo];
}

let result = getWeekNumber();

if (result[1] % 2 == 0) {
    // Set the date we're counting down to    
    d.setDate(d.getDate() + (1 + 7 - d.getDay()) % 7);
    let countDownDate = d.setHours(0, 0, 0, 0);

    // Update the count down every 1 second
    setInterval(function () {
        // Get today's date and time
        let now = new Date().getTime();

        // Find the distance between now and the count down date
        let distance = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        let days = Math.floor(distance / (1000 * 60 * 60 * 24));
        let hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        let minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        let seconds = Math.floor((distance % (1000 * 60)) / 1000);

        // Output the result in an element with id="countdown"
        document.getElementById("countdown").innerText = days + " dag(e) " + hours + " time(r) "
            + minutes + " minut(ter) " + seconds + " sekund(er) ";

    }, 1000);
}
else {
    document.getElementById("countdown").innerText = "Afstemning er udløbet";
}
