<template>
    <v-app>
      <v-main>
        <HeroComponent/>
        <Service/>
        <AboutUs/>
        <ContactUs/>
      </v-main>
    </v-app>
  </template>
  
  <script>
  import HeroComponent from "@/components/user/HeroComponent"
  import Service from "@/components/user/Service"
  import AboutUs from "@/components/user/AboutUs"
  import ContactUs from "@/components/user/ContactUs"

  export default{
    name:'App',
    components:{
      HeroComponent,
      Service,
      AboutUs,
      ContactUs,
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