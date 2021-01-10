<template>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
  <CreateFeedback/>
  <h1>User Feedbacks</h1>
  <div id="feedbackList" class="list-group container w-50" >
       <FeedbackItem name="feedback" v-for="feedback in feedbacks" v-bind:key="feedback.Id" v-bind:feedback="feedback" v-on:update-feedbacks="getAllFeedbacks"></FeedbackItem>
  </div>
</template>

<script>
import FeedbackItem from '../components/FeedbackItem'
import CreateFeedback from '../views/CreateFeedback'
import axios from 'axios';
import api from '../constant/api.js'

export default {
  name:'ObserveFeedback',
  data: function()
  {
      return{
        feedbacks:[],
      }
  },
  components:
  {
     FeedbackItem, CreateFeedback
  },
  methods: 
  {
    getAllFeedbacks:function()
    {
      let url = api.feedback
      if (this.$store.state.user && this.$store.state.user.role !== "Admin")
        url +="/published"

      axios.get(url).then((response) => 
      {
         response.data.forEach(element => {
           element.date = this.formatDate(element.date)
         });
         this.feedbacks = response.data
      })      
    },
    formatDate : function(date) {
      let d = new Date(date)
      return d.getDate() + "/" + (d.getMonth()+1) + "/" + d.getFullYear()
    }
  },
  mounted: function()
  {
    this.getAllFeedbacks();
  }
}
</script>