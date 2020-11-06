<template>
  <h1>User Feedbacks</h1>
  <div id="feedbackList" class="list-group container w-50" >
       <FeedbackItem v-for="feedback in feedbacks" v-bind:key="feedback.Id" v-bind:feedback="feedback"></FeedbackItem>
  </div>
</template>

<script>

import FeedbackItem from '../components/FeedbackItem.vue'
import axios from 'axios';


export default {
  name:'ObserveFeedback',
  data: function()
  {
      return{
        feedbacks:[]
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
      axios.get("http://localhost:5290/feedback").then((response) => 
      {
         this.feedbacks = response.data
         console.log(response.data)
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