<template>

    <div class="container">
        <div class="col align-items-center justify-content-center">
            <div class="title">
                <h2> Survey preview </h2>
            </div>
            <div class="divider div-transparent"></div>

            <div id="accordion" class="col " v-if="is_survey_fetched">
                <div v-for="(surveySection,index) in survey.surveySections" v-bind:key="index"
                    class="d-flex justify-content-center">
                    <div class="card text-left w-50 row mb-2">
                        <div class="card-header ">
                            <div class="row ml-1">
                                <div class="col">{{surveySection.sectionName}}</div>
                                <div class="col">
                                    <div class="row">
                                        <StarRating :star-size="20" :show-rating="false" :inline="true"
                                            v-model:rating="surveySection.averageRating"></StarRating>
                                        <div class="col mt-1">
                                            {{surveySection.averageRating+"/"+"5"}}
                                        </div>
                                        
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <ul class="list-group" v-for="(question,index) in surveySection.surveyQuestions"
                                v-bind:key="index">
                                <li class="list-group-item">
                                    <div class="col">
                                        <div class="row">
                                            {{question.question}}
                                        </div>
                                        <div class="row">
                                                <StarRating v-bind:star-size="15" :increment=0.01 :show-rating="false"
                                                    :inline="true"
                                                    v-model:rating="question.questionAverage"></StarRating>
                                                <div class="ml-1">
                                                    {{question.questionAverage + "/5" }}
                                                </div>
                                        </div>
                                        <hr class="my-4">
                                        <div class="row" v-for="(rating,index) in question.ratingsCount" v-bind:key="index">
                                            <div class="col-sm-3">
                                               {{index + " Stars"}}
                                            </div>
                                            <div class="col">
                                                <div class="progress">
                                                    <div class="progress-bar" role="progressbar" v-bind:aria-valuenow="calculatePercentage(question.ratingsCount,index)" v-bind:style="{ width: calculatePercentage(question.ratingsCount,index) + '%'}"  aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                            <div class="col">
                                                {{calculatePercentage(question.ratingsCount,index) + "%"}}
                                            </div>
                                        </div>

                                    </div>
                                </li>
                            </ul>
                        </div>

                    </div>
                </div>
                <div v-for="(doctorSection,index) in survey.doctorSurveySections" v-bind:key="index"
                    class="d-flex justify-content-center">
                    <div class="card text-left w-50 row mb-2">
                        <div class="card-header">

                            <div class="row ml-1">
                                <div class="col">{{doctorSection.sectionName + " " + doctorSection.doctorName}}</div>
                                <div class="col">
                                    <div class="row">
                                        <StarRating :star-size="20" :show-rating="false" :inline="true"
                                            v-model:rating="doctorSection.averageRating"></StarRating>
                                        <div class="col mt-1">
                                            {{doctorSection.averageRating+"/"+"5"}}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <ul class="list-group" v-for="(question,index) in doctorSection.surveyQuestions"
                                v-bind:key="index">
                                <li class="list-group-item">
                                    <div class="col">
                                        <div class="row">
                                            {{question.question}}
                                        </div>
                                        <div class="row">

                                                <StarRating v-bind:star-size="15" :increment=0.01 :show-rating="false"
                                                    :inline="true"
                                                    v-model:rating="question.questionAverage"></StarRating>
                                                <div class="ml-1">
                                                    {{question.questionAverage + "/5" }}
                                                </div>
                                        </div>
                                        <hr class="my-4">
                                        <div class="row" v-for="(rating,index) in question.ratingsCount" v-bind:key="index">
                                            <div class="col-sm-3">
                                               {{index + " Stars"}}
                                            </div>
                                            <div class="col">
                                                <div class="progress">
                                                    <div class="progress-bar" role="progressbar" v-bind:aria-valuenow="calculatePercentage(question.ratingsCount,index)" v-bind:style="{ width: calculatePercentage(question.ratingsCount,index) + '%'}"  aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                            <div class="col">
                                                {{calculatePercentage(question.ratingsCount,index) + "%"}}
                                            </div>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
</template>

<script>
import axios from 'axios'
import api from '../constant/api.js'
import StarRating from 'vue-star-rating'

export default {
    name:"SurveyPreview",
    data:function(){
        return{
            survey:null,
            is_survey_fetched:false,
            
        }
    },
    components:{StarRating},
    methods: {
        getSurvey:function(){
            let url = api.surveyPreview
            axios
                .get(url)
                .then((res)=>{
                    this.survey=res.data
                    this.is_survey_fetched=true
                })

        },
       calculatePercentage:function(ratingsCount,rating){
            let rate = parseInt(ratingsCount[rating])
            let sum=0
            let values = Object.values(ratingsCount)
            
            for(var value of values){
                sum = sum + parseInt(value)
            }
            if(sum==0)
                return 0

            return (rate / sum ) * 100


        }
    },
    mounted:function(){
        this.getSurvey();
        
    }

    
}
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Advent+Pro:wght@100&display=swap');
.title{
    position:relative;
    top:10%;
    font-family: 'Advent Pro',sans-serif;
}
.divider
{
	position: relative;
	margin-top: 25px;
	height: 25px;
}

.div-transparent:before
{
	content: "";
	position: absolute;
	top: 0;
	left: 35%;
	right: 5%;
	width: 30%;
	height: 1px;
	background-image: linear-gradient(to right, transparent, rgb(48,49,51), transparent);
}



</style>