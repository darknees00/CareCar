<template>
    <v-app id="inspire">
        <!-- <Headerr @handeDrawer="drawer = !drawer"/> -->
        <Sidebar/>
        <v-main>
            <router-view/>
        </v-main>
    </v-app>
</template>
<script>
// import Headerr from "@/components/admin/Header"
import Sidebar from "@/components/admin/Sidebar"
export default{
  components:{
    // Headerr,
    Sidebar
  },
  data: ()=>({

  })
  
};
</script>