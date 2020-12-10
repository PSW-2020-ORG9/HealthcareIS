<template>
<body>
  <div class="bg-image">
  <div id="nav" style="background-color:rgb(177, 255, 177); margin-bottom: 50px;">
    <router-link to="/" style="margin-right: 5px; margin-left: 5px;">Observe feedback</router-link> &middot;
    <router-link to="/create-feedback" style="margin-right: 5px; margin-left: 5px;">Create feedback</router-link> &middot;
    <router-link to="/survey-preview" style="margin-right: 5px; margin-left: 5px;">Survey preview</router-link> &middot;
    <router-link to="/medical-record" style="margin-right: 5px; margin-left: 5px;"> Observe medical record </router-link> &middot;
    <router-link to="/doc-search" style="margin-right: 5px; margin-left: 5px;">Search medical documentation </router-link> &middot;
    <router-link to="/register" style="margin-right: 5px; margin-left: 5px;">Patient registration </router-link> &middot;
    <router-link to="/examinations" style="margin-right: 5px; margin-left: 5px;">Patient examinations </router-link>
    <router-link to="/schedule" style="margin-right: 5px; margin-left: 5px;">Schedule Appointment </router-link>
    <router-link to="/scheduling-type" style="margin-right: 5px; margin-left: 5px;">Schedule Appointment </router-link>
  </div>
  <router-view/>
  </div>
</body>
</template>

<style lang="scss">
body {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  min-height: 100vh;
  text-align: center;
  color: #2c3e50;

}
#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #285228;

    &.router-link-exact-active {
      color: #273f27;
      font-size: 18px;
      font-weight: bold;
    }
  }
}

</style>
