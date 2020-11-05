<template>
  <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">

  <div class="container">
    <div class="col-sm-3 mx-auto shadow-lg p-3 mb-5 bg-white rounded feedback">
      <div class="col-sm d-flex justify-content-start">
        <div class="form-group d-flex justify-content-start">
          <textarea class="form-control input-feedback" rows=5 type="text" placeholder="Enter your feedback" v-model="feedbackContent" required></textarea>
        </div>
      </div>
      <div class="col-sm offset-md-1 d-flex justify-content-start">
        <div class="form-group">
          <input type="checkbox" class="form-check-input" id="public-feedback">
          <label class="form-check-label" for="public-feedback">Public</label>
        </div>
      </div>
      <div class="col-sm offset-md-1 d-flex justify-content-start">
        <div class="form-group">
          <input type="checkbox" class="form-check-input" id="anon-feedback">
          <label class="form-check-label" for="anon-feedback">Send anonimously</label>
        </div>
      </div>
      <button :disabled="!feedbackContent" type="submit" class="btn submit-btn" @click="onSubmit()" >Submit</button>
    </div>
  </div>
  
</template>

<script>

  import api from '../constant/api.js'
  import axios from 'axios'
  import Toastify from 'toastify-js'

  export default {
    name: 'CreateFeedback',
    data: function () {
      return {
        feedbackContent: "",
        anonymous: false,
        public: false,
        userId: 1,
        buttonEnabled: false

      }

    },
    methods: {
      toastSuccess:function(){
        Toastify({
              text: "Feedback succesfully added!",
              duration: '2000',
              newWindow: true,
              close: true,
              gravity: 'top',
              position: 'center',
              backgroundColor: "linear-gradient(to right, #00b09b, #7ecc92)"
            }).showToast()
      },
      toastError:function(){
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
      postFeedback:function(userFeedback){
        axios.post(api.feedback, userFeedback)
          .then((res) => {
            this.toastSuccess()
          })
          .catch((err) => {
            this.toastError()
          })
      },
      onSubmit: function () {

        var userFeedback = {
          Date: new Date(),
          UserComment: this.feedbackContent,
          isPublic: this.public,
          isAnonymous: this.isAnonymous,
          UserId: this.userId
        }

        this.postFeedback(userFeedback)
    
      },
      
    }
  }

</script>

<style>

.submit-btn {
  background-color: #58804f;
  transition-duration: 0.2s;
  border: 2px solid #58804f;
}

.submit-btn:hover {
  background-color: white;
  color: #58804f;
}


</style>