<template>

 <div class="item card">
     <div class="d-flex justify-content-between bg-info">
        <h5 class="card-header text-left  text-white">{{feedback.user.username}}</h5>
        <p class="card-header  text-white text-right">{{feedback.date}}</p>
     </div>
        <div class="card-body">           
            <p class="text-left card-text">{{feedback.userComment}}</p>      
        </div>
 </div>
 
</template>

<script>
export default {
  name:'FeedbackItem',
  props: {
    feedback: Object
  }
}
</script>

<style>

    .item{
        margin-top: 15px;
    }
    

</style>