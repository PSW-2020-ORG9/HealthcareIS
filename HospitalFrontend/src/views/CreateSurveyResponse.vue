<template>
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
    <h1>Patient Survey</h1>

    <h2>How satisfied or dissatisfied are you with the following: </h2>

    <div v-if="survey" id="surveysectionlist" class="container w-50">
       <div class="bg-light rounded" v-for="surveySection in survey.surveySections" v-bind:key="surveySection.Id" v-bind:surveySection="surveySection" >
            <div class="list-group my-4">
                <div class="bg-secondary col d-flex justify-content-left rounded-top">
                    <h5 class="text-left text-white m-1">{{surveySection.sectionName}}</h5>
                </div>
                <div class="mb-3" v-for="surveyQuestion in surveySection.surveyQuestions" v-bind:key="surveyQuestion.id">
                    <div class="my-1"> {{surveyQuestion.question}} </div>
                    <div class="form-check form-check-inline">
                        <input type="radio" class="form-check-input ml-2" v-bind:name="'group' + surveyQuestion.id" 
                            v-on:click="rateQuestion(surveySection, surveyQuestion.id, 1)">
                        <label class="form-check-label">1</label>
                        <input type="radio" class="form-check-input ml-2" v-bind:name="'group' + surveyQuestion.id"
                            v-on:click="rateQuestion(surveySection, surveyQuestion.id, 2)">
                        <label class="form-check-label">2</label>
                        <input type="radio" class="form-check-input ml-2" v-bind:name="'group' + surveyQuestion.id"
                            v-on:click="rateQuestion(surveySection, surveyQuestion.id, 3)">
                        <label class="form-check-label">3</label>
                        <input type="radio" class="form-check-input ml-2" v-bind:name="'group' + surveyQuestion.id"
                            v-on:click="rateQuestion(surveySection, surveyQuestion.id, 4)">
                        <label class="form-check-label">4</label>
                        <input type="radio" class="form-check-input ml-2" v-bind:name="'group' + surveyQuestion.id"
                            v-on:click="rateQuestion(surveySection, surveyQuestion.id, 5)">
                        <label class="form-check-label">5</label>
                    </div>
                </div>
            </div>
        </div>
        <button type="button" class="btn btn-success btn-lg mb-5" v-on:click="saveSurveyResponse">Submit survey</button>
    </div>

    
</template>

<script>

import axios from 'axios'
import api from '../constant/api.js'
import Toastify from 'toastify-js'



export default {

    name:'CreateSurveyResponse',
    data:function() {
        return{          
            survey: null,
            surveyResponse: {
                surveyId:null,
                ratedSurveySections:[],
                doctorSurveySection:null,
                patientAccountId:1,
                doctorId: this.$route.params.doctor
            }
        }
    },
    methods: {
        getSurvey: function () {
            axios.get(api.survey + '/find/1').then((response)=>{
                this.survey = response.data;
                this.surveyResponse.surveyId = this.survey.id
            })
        },
        rateQuestion: function (section, questionId, rating) {
            if (section.isDoctorSection) {
                if (this.surveyResponse.doctorSurveySection == null) {
                    this.surveyResponse.doctorSurveySection = {
                        doctorId: this.doctorId,
                        surveySectionId: section.id,
                        ratedSurveyQuestions: [{
                            surveyQuestionId: questionId,
                            rating: rating
                        }]
                    }
                    return
                }
                for(let i = 0; i < this.surveyResponse.doctorSurveySection.ratedSurveyQuestions.length; i++) {
                    if(this.surveyResponse.doctorSurveySection.ratedSurveyQuestions[i].surveyQuestionId == questionId) {
                        this.surveyResponse.doctorSurveySection.ratedSurveyQuestions[i].rating = rating
                        return
                    }
                }
                this.surveyResponse.doctorSurveySection.ratedSurveyQuestions.push({
                            surveyQuestionId: questionId,
                            rating: rating
                        })
                return
            }
            for(let i = 0; i < this.surveyResponse.ratedSurveySections.length; i++) {
                if (this.surveyResponse.ratedSurveySections[i].surveySectionId == section.id) {
                    if(this.surveyResponse.ratedSurveySections[i].ratedSurveyQuestions != []) {
                        for(let j = 0; j < this.surveyResponse.ratedSurveySections[i].ratedSurveyQuestions.length; j++) {
                            if (this.surveyResponse.ratedSurveySections[i].ratedSurveyQuestions[j].surveyQuestionId == questionId) {
                                this.surveyResponse.ratedSurveySections[i].ratedSurveyQuestions[j].rating = rating
                                return
                            }
                        }
                    }
                    this.surveyResponse.ratedSurveySections[i].ratedSurveyQuestions.push({
                        surveyQuestionId: questionId,
                        rating: rating
                    })
                    return
                }
            }
            this.surveyResponse.ratedSurveySections.push({
                surveySectionId: section.id,
                ratedSurveyQuestions: [{
                        surveyQuestionId: questionId,
                        rating: rating
                    }
                ]
            })
        },
        saveSurveyResponse: function () {
            axios.post(api.survey + '/response', this.surveyResponse)
            .then(response => this.toastSuccess())
            .catch(err => this.toastError())
        },
        toastSuccess: function () {
            Toastify({
                text: "Survey succesfully sent!",
                duration: '2000',
                newWindow: true,
                close: true,
                gravity: 'top',
                position: 'center',
                backgroundColor: "linear-gradient(to right, #00b09b, #7ecc92)"
            }).showToast()
        },
        toastError: function () {
            Toastify({
                text: "Error while sending survey!",
                duration: '2000',
                newWindow: true,
                close: true,
                gravity: 'top',
                position: 'center',
                backgroundColor: "linear-gradient(to right, #C37D92, #d89a9e)"
            }).showToast()
        },
    },
    mounted: function () {
        this.getSurvey();
    } 
}
</script>

<style>

</style>