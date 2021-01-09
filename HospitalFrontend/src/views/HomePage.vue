<template>

   <carousel v-if="advertisements.length!=0">
    <slide v-for="ad in advertisements" :key="ad.Id">
        <div class="carousel__item">
            <img class="carousel__pic" v-bind:src="ad.pictureUrl" />
            <div class="carousel__text">
                <h2>{{ad.header}}</h2>
                <p>{{ad.content}}</p>
            </div>
        </div>
    </slide>

    <template #addons>
      <navigation />
    </template>
  </carousel>
</template>

<script>
import axios from 'axios'
import api from '../constant/api.js'
import 'vue3-carousel/dist/carousel.css'
import {Carousel,Slide,Navigation} from 'vue3-carousel'

export default {
    name:'HomePage',
    data:function(){
        return{
            advertisements:[]
        }
    },
    components:{Carousel,Slide,Navigation},
    methods: {
      fetchAdvertisements:function(){
          axios.get(api.advertisement).then(response=>{
              this.advertisements = response.data
          })
      }  
    },
    mounted(){
        this.fetchAdvertisements()
    }
}
</script>

<style>
.carousel{
    position:fixed;
    top:50%;
    left:50%;
    transform: translate(-50%, -50%);
    width: 70%;
}
.carousel__item {
  min-height: 200px;
  width: 100%;
  background-color: #bdd8cab6;
  color: white;
  font-size: 20px;
  border-radius: 8px;
  display: flex;
  justify-content: start;
  align-items: center;
}
.carousel__pic{
    display: flex;
    justify-content: start;
    align-items: left;
    width: 500px;
    height: 300px;
    -moz-box-shadow: 1px 2px 3px rgba(0,0,0,.5);
    -webkit-box-shadow: 1px 2px 3px rgba(0,0,0,.5);
    box-shadow: 1px 2px 3px rgba(0,0,0,.5);
}
.carousel__slide {
  padding: 10px;
}
.carousel__text h2{
    color:rgb(3, 15, 2);
}
.carousel__text p {
    padding: 5%;
    color:rgb(65, 80, 63);
}

.carousel__prev,
.carousel__next {
  box-sizing: content-box;
  border: 5px solid white;
  background-color: #2afb92;
}
</style>