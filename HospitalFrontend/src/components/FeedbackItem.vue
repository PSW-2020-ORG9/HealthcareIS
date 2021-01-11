<template>
  <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">

  <div class="item card" name="feedback">
    <div class="d-flex justify-content-between bg-info">
      <h5 v-if="!feedback.feedbackVisibility.isAnonymous" class="card-header text-left  text-white">{{feedback.patientAccount.credentials.username}}</h5>
      <h5 v-else class="card-header text-left  text-white">Anonymous</h5>
      <p class="card-header  text-white text-right">{{feedback.date}}</p>
    </div>
    <div class="card-body">           
        <div class="text-left">{{feedback.userComment}}</div>
        <button v-if="$store.state.user && $store.state.user.role=='Admin' 
        & !feedback.feedbackVisibility.isPublished 
        & feedback.feedbackVisibility.isPublic" 
        @click="publishFeedback" type="button" class="btn btn-success float-right" v-bind:id="'publish' + feedback.id">Publish</button>
    </div>
  </div>

</template>

<script>
import axios from 'axios'
import api from '../constant/api.js'
import Toastify from 'toastify-js'

export default {
  name:'FeedbackItem',
  props: {
    feedback: Object,
    user: String
  },
  emits : [
    "updateFeedbacks"
  ],
  methods: {
    publishFeedback: function () {
      axios.get(api.feedback + '/publish/' + this.feedback.id)
        .then(response => {
          this.$emit('update-feedbacks')
          this.toastSuccess()
        }).catch(error => {
          this.toastError(error.response.data)
        })
    },
    toastSuccess: function () {
        Toastify({
              text: "Feedback succesfully published!",
              duration: '2000',
              newWindow: true,
              close: true,
              gravity: 'top',
              position: 'center',
              backgroundColor: "linear-gradient(to right, #00b09b, #7ecc92)"
            }).showToast()
      },
      toastError: function (message) {
        Toastify({
              text: message,
              duration: '2000',
              newWindow: true,
              close: true,
              gravity: 'top',
              position: 'center',
              backgroundColor: "linear-gradient(to right, #C37D92, #d89a9e)"
            }).showToast()
      },
  }
}
</script>

<style>
    .item{
        margin-top: 15px;
    }
</style>