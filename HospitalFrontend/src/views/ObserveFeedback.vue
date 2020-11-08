<template>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
<!--Radio buttons for choosing role -->
  <div class="col">
    <div class="form-check form-check-inline">
      <input class="form-check-input" type="radio" id="userRadio" value="user" v-model="user">
      <label class="form-check-label" for="userRadio">Regular user</label>
    </div>
    <div class="form-check form-check-inline">
      <input class="form-check-input" type="radio" id="adminRadio" value="admin" v-model="user">
      <label class="form-check-label" for="adminRadio">Admin</label>
    </div>
  </div>

  <h1>User Feedbacks</h1>
  <div id="feedbackList" class="list-group container w-50" >
       <FeedbackItem v-for="feedback in feedbacks" v-bind:key="feedback.Id" v-bind:feedback="feedback" v-bind:user="user" v-on:update-feedbacks="getAllFeedbacks"></FeedbackItem>
  </div>
</template>

<script>
import FeedbackItem from '../components/FeedbackItem.vue'
import axios from 'axios';
import api from '../constant/api.js'

export default {
  name:'ObserveFeedback',
  data: function()
  {
      return{
        feedbacks:[],
        user: 'user'
      }
  },
  components:
  {
     FeedbackItem
  },
  methods: 
  {
    getAllFeedbacks:function()
    {
      axios.get(api.feedback).then((response) => 
      {
         this.feedbacks = response.data
      })      
    }
  },
  mounted: function()
  {
    this.getAllFeedbacks();
  }
}
</script>

<style>

</style>