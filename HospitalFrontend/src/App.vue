<template>
  <div class="bg-image"></div>
  <div id="nav">
    <router-link to="/">Observe feedback</router-link> |
    <router-link to="/create-feedback">Create feedback</router-link> 
  </div>
  <router-view/>
</template>

<style lang="scss">
#app {
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
    color: #2c3e50;

    &.router-link-exact-active {
      color: #58804f;
    }
  }
}
</style>
