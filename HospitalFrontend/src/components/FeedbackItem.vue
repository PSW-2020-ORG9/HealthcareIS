<template>

 <div class="item card">
    <div class="d-flex justify-content-between bg-info">
      <h5 v-if="!feedback.isAnonymous" class="card-header text-left  text-white">{{feedback.user.username}}</h5>
      <h5 v-else class="card-header text-left  text-white">Anonymous</h5>
      <p class="card-header  text-white text-right">{{feedback.date}}</p>
    </div>
    <div class="card-body">           
        <div class="text-left">{{feedback.userComment}}</div>
        <button v-if="user=='admin' & !feedback.isPublished & !feedback.isAnonymous & feedback.isPublic" @click="publishFeedback" type="button" class="btn btn-success float-right">Publish</button>
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
  methods: {
    publishFeedback: function () {
      let publishedFeedback = JSON.parse(JSON.stringify(this.feedback))
      publishedFeedback.user = null
      publishedFeedback.isPublished = true
      axios.put(api.feedback, publishedFeedback)
        .then(response => {
          this.$emit('update-feedbacks')
          this.toastSuccess()
        }).catch(error => {
          this.toastError()
        })
    },
    toastSuccess: function () {
        Toastify({
              text: "Feedback succesfully publish!",
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
              text: "An error ocurred!",
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