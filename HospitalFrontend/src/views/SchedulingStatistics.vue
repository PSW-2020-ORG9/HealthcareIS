<template>
    <div class="container">
        <div class="row">
            <div class="col">
                <canvas id="steps-stats" width="400" height="400"></canvas>
            </div>
            <div class="col">
                <canvas id="success-ratio" width="400" height="400"></canvas>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <canvas id="step-duration" width="400" height="400"></canvas>
            </div>
            <div class="col">
                <canvas id="age-duration" width="400" height="400"></canvas>
            </div>
        </div>
    </div>
</template>
<script>
import Chart from 'chart.js'
import axios from 'axios'
import api from '../constant/api.js'
import moment from 'moment'
    export default {
        name: 'SchedulingStatistics',
        data:function(){
            return{

            }
        },
        mounted() {
            this.fetchStepStats()
            this.fetchSuccessRatio()
            this.fetchStepDurationStats()
            this.fetchAgeStats()

        },
        methods:{
            fetchStepStats:function(){
                axios.get(api.schedulingStepsStatistics).then(response=>{
                    this.formStepsChart(response.data)
                })
            }
            ,
            fetchSuccessRatio:function(){
                 axios.get(api.schedulingSuccessRate).then(response=>{
                    this.formSuccessRatioChart(response.data)
                })
            }
            ,
            fetchStepDurationStats:function(){
                axios.get(api.schedulingStepDurationStatistics).then(response=>{
                    this.formStepDurationChart(response.data)
                })
            }
            ,
            fetchAgeStats:function(){
                axios.get(api.schedulingAgeStatistics).then(response=>{
                    this.formAgeChart(response.data)
                })
            }
            ,
            formAgeChart:function(data){
                let chartData=[]
                for(const property in data)
                    chartData.push({x:parseInt(property),
                    y:moment.duration(data[property]).asSeconds()})


                var ctx = document.getElementById('age-duration');
                var ageStatsChart = new Chart(ctx,{
                    type:'line',
                    data:{
                        labels:[18,34,54,74,84]
                        ,
                        datasets:[{
                            data:chartData,
                            backgroundColor:'rgba(54, 162, 235, 0.6)'
                        }]
                    }
                    ,
                    options:{
                        scales:{
                            yAxes:[{
                                scaleLabel:{
                                    display:true,
                                    labelString:'average time(in seconds) needed to schedule an examination'
                                }
                            }]
                            ,
                            xAxes:[{
                                scaleLabel:{
                                    display:true,
                                    labelString:'patients age'
                                }
                            }]
                        },
                        title:{
                            display:true,
                            text:'Comparison of users age and average time needed to schedule an examination'
                        }
                        ,
                        legend:{
                            display:false
                        }
                    }
                })

            }
            ,
            formStepDurationChart:function(data){
                let chartData = {
                    "Date selection step":0,
                    "Doctor selection step":0,
                    "Appointment selection step":0
                }

                for(const property in data){
                    if(property.includes("0"))
                        chartData["Date selection step"] = moment.duration(data[property]).asSeconds()
                    else if(property.includes("1"))
                        chartData["Doctor selection step"] = moment.duration(data[property]).asSeconds()
                    else
                        chartData["Appointment selection step"] = moment.duration(data[property]).asSeconds()
                }

                var ctx = document.getElementById('step-duration');
                var stepStatsChart = new Chart(ctx,{
                    type:'bar',
                    data: {
                        labels : Object.keys(chartData),
                        datasets:[{
                            label: 'Number of seconds',
                            data: Object.values(chartData),
                            backgroundColor:[
                                'rgba(255, 99, 132, 0.6)',
                                'rgba(54, 162, 235, 0.6)',
                                'rgba(255, 206, 86, 0.6)'
                            ]
                        }]
                    },
                    options:{
                        title:{
                            display:true,
                            text:'Average number of seconds needed for each step'
                        }
                        ,
                        legend:{
                            display:false
                        }
                        ,
                        scales:{
                            yAxes:[{
                                display: true,
                                ticks:{
                                    suggestedMin:0
                                }
                            }]
                        }
                    }
                })

            }
            ,
            formSuccessRatioChart:function(data){
                let chartData = {
                    "Successfully scheduled":0,
                    "Unsuccessfully scheduled":0,
                }
                for(const property in data){
                    if(property.includes("success"))
                        chartData["Successfully scheduled"] = data[property]
                    else 
                        chartData["Unsuccessfully scheduled"] = data[property]
                }
                var ctx = document.getElementById('success-ratio');
                var successPie = new Chart(ctx,
                {
                    type:'pie',
                    data: {
                        labels : Object.keys(chartData),
                        datasets:[{
                            data: Object.values(chartData),
                            backgroundColor:[
                                'rgba(255, 99, 132, 0.6)',
                                'rgba(54, 162, 235, 0.6)',
                                'rgba(255, 206, 86, 0.6)'
                            ]
                        }]
                    },
                     options:{
                        title:{
                            display:true,
                            text:'Successfully/unsuccessfully scheduled ratio'
                        }
                     }

                })
            }
            ,
            formStepsChart:function(data){
                let chartData = {
                    "Min":0,
                    "Avg":0,
                    "Max":0
                }
                for(const property in data){
                    if(property.includes("min"))
                        chartData["Min"] = data[property]
                    else if(property.includes("avg"))
                        chartData["Avg"] = data[property]
                    else
                        chartData["Max"] = data[property]
                }
                var ctx = document.getElementById('steps-stats');
                var stepStatsChart = new Chart(ctx,{
                    type:'bar',
                    data: {
                        labels : Object.keys(chartData),
                        datasets:[{
                            label: '# of Steps',
                            data: Object.values(chartData),
                            backgroundColor:[
                                'rgba(255, 99, 132, 0.6)',
                                'rgba(54, 162, 235, 0.6)',
                                'rgba(255, 206, 86, 0.6)'
                            ]
                        }]
                    },
                    options:{
                        title:{
                            display:true,
                            text:'Minimum, average and maximum number of steps resulted in a successful scheduling'
                        }
                        ,
                        legend:{
                            display:false
                        }
                        ,
                        scales:{
                            yAxes:[{
                                display: true,
                                ticks:{
                                    suggestedMin:0
                                }
                            }]
                        }
                    }
                })
            }
        }
    }
</script>

<style>

</style>