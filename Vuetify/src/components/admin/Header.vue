<template>
    <v-app-bar app
        color="while"
        elevate-on-scroll
        elevation="4"
      >
        <v-app-bar-nav-icon @click="$emit('handeDrawer')"></v-app-bar-nav-icon>

        <v-spacer/>
        <v-col lg="6" cols="12">
            <v-form class="mt-8">
                <v-text-field rounded outline dense  placeholder="Searh here" append-icon="mdi-magnify"/>
            </v-form>
        </v-col>
        <v-spacer/>

       
      </v-app-bar>
</template>