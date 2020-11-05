<template>
  <h1>This is a ObserveFeedback page for any user and admin </h1>
  <div id="feedbackList" class="list-group">
    <FeedbackItem v-for="feedback in feedbacks" v-bind:key="feedback.Id" feedback ="feedback"></FeedbackItem>


  </div>
</template>

<script>
import FeedbackItem from './components/FeedbackItem.vue'

export default {
  name:'ObserveFeedback',
  data: function(){
      return{
        feedbacks:[]

      }
  },
  components:{
    FeedbackItem
  },
  methods: {},
  mounted: function(){
    
      
    
  }
}
</script>

<style>

</style>