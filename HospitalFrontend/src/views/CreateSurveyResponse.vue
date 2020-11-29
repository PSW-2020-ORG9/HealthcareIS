<template>

    <h1>Patient Survey</h1>

    <h2>How satisfied or dissatisfied are you with the following: </h2>

    <div v-if="survey" id="surveysectionlist" class="container w-50" >
       <div v-for="surveySection in survey.surveySections" v-bind:key="surveySection.Id" v-bind:surveySection="surveySection" >

            <div  id="surveyquestionslist" class="list-group container my-4">
        <table class="table">
            <thead class="thead-dark">
                <tr>
                    <th class="text-left" scope="col">{{surveySection.sectionName}}</th>
                </tr>
            </thead>
        </table>
        <div class="mb-3" v-for="surveyQuestion in surveySection.surveyQuestions" v-bind:key="surveyQuestion.id">
           <div class="text-justify"> {{surveyQuestion.question}} </div>
                <div class="form-check form-check-inline text-right">
                    <input class="form-check-input" type="radio" v-bind:name=" 'radiogroup' +surveyQuestion.id" id="radio1" value="1">
                    <label class="form-check-label" for="radio1">1</label>
                </div>
                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="radio" v-bind:name="'radiogroup'+surveyQuestion.id" id="radio2" value="2">
                    <label class="form-check-label" for="radio2">2</label>
                </div>
                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="radio" v-bind:name="'radiogroup'+surveyQuestion.id" id="radio3" value="3" >
                    <label class="form-check-label" for="radio3">3</label>
                </div>
                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="radio" v-bind:name="'radiogroup'+surveyQuestion.id" id="radio4" value="4" >
                    <label class="form-check-label" for="radio4">4</label>
                </div>
                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="radio" v-bind:name="'radiogroup'+surveyQuestion.id" id="radio5" value="5" >
                    <label class="form-check-label" for="radio5">5</label>
                </div>
             </div>
         </div>





       </div>
    </div>
    

    <button type="button" class="btn btn-primary btn-lg">Submit survey</button>
</template>

<script>

import axios from 'axios'
import api from '../constant/api.js'



export default {

    name:'CreateSurveyResponse',
    data:function(){
        return{          
            survey: {
                SurveySections:[],
                Id:null
            },

            surveyResponse:
            {
                SubmittedAt:"",
                SurveyId:null,
                RatedSurveySections:null,
                DoctorSurveySection:null,
                PatientAccountId:1


            }
        }

    },
    components:{
        
    },
    methods:
    {
        getSurvey:function()
        {
            axios.get(api.survey + '/find/1').then((response)=>{
                this.survey = response.data;
            })
        },
    },
    mounted:function(){
        this.getSurvey();
    }




    
}
</script>

<style>

</style>