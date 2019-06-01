<template>
    <div>
         <b-modal  v-model="modalShow"  @hidden="resetModal" centered >
             <!-- <b-img :src="this.getData.url" fluid alt="Responsive image"></b-img> -->
                    <p class="my-4">{{this.getData.name}}</p>
            </b-modal>
    </div>
</template>

<script>
 import { mapActions, mapGetters } from "vuex";

export default {

    data(){
        return{
            modalShow : true
        }
    },
    computed: {
        ...mapGetters(['index', 'activeView', 'movies', 'actors', 'producers']),
        getData(){
            switch(this.activeView){
                case 'movie' :
                    return this.movies[this.index];
                    break;
                case 'actors' :
                    return this.actors[this.index];
                    break;
                case 'producers' : 
                    return this.producers[this.index];
            }
        }
    },
    methods : {
        ...mapActions(['hideShowDetails']),
        resetModal(){
            this.hideShowDetails();
        }
    }
    
    
}
</script>
