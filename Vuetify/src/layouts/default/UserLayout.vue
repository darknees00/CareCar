<template>
  <v-app>
    <Headerr/>   
    <v-main>
      <router-view />
    </v-main>
    <Footerr/>
  </v-app>
</template>

<script>
import Headerr from "@/components/user/Header"
import Footerr from "@/components/user/Footer"
export default{
  name:'App',
  components:{
    Headerr,
    Footerr,
  },
  
};
</script>
<style>
  .v-toolbar-title{
    font-size: 32px;
    color: #4caf50;
    font-weight: bold;
    font-family: cursive;
  }
</style>