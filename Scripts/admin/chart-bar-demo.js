// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

var DataBar = [] ;

// Ajax
$(document).ready(function() {
  $.ajax({
    type: "GET",
    url: "/Admin/ApiTotal",
    dataType: "json",
    async: false,
    success: function (data) {
      var arr = data.data;
      for (let i =0;i<arr.length;i++)
      {
        DataBar.push(arr[i]);
      }
    },
    error: function () {
      console.log("err");
    }
  });
});

console.log(DataBar);

// Bar Chart Example
var ctx = document.getElementById("myBarChart");
var myLineChart = new Chart(ctx, {
  type: 'bar',
  data: {
    labels: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
    datasets: [{
      label: "Revenue",
      backgroundColor: "rgba(2,117,216,1)",
      borderColor: "rgba(2,117,216,1)",
      data: DataBar,
    }],
  },
  options: {
    scales: {
      xAxes: [{
        time: {
          unit: 'month'
        },
        gridLines: {
          display: false
        },
        ticks: {
          minTicksLimit: 12
        }
      }],
      yAxes: [{
        ticks: {
          min: 0,
          max: 2000,
          minTicksLimit: 5
        },
        gridLines: {
          display: true
        }
      }],
    },
    legend: {
      display: false
    }
  }
});
