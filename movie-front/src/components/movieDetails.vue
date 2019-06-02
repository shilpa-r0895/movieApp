<template>
    <div>
        <b-modal  v-model="modalShow" class="form" @hidden="cancel()" centered title="Enter Movie Details">
         
            <b-form-group
                label="Name"
                col="4"
                label-for="name-input"
                label-align ="left"
                invalid-feedback="Name is required"
            >
                <b-form-input
                    id="name-input"
                    v-model="name"
                    
                    required
                ></b-form-input>
            </b-form-group>
            <b-form-group
                label="Plot"
                label-align ="left"
                label-for="plot-input"
                invalid-feedback="Plot is required"
            >
                <b-form-input
                    id="plot-input"
                    v-model="plot"
                    col="4"
                    required
                ></b-form-input>
            </b-form-group>
            <b-form-group
                label="Image URL"
                label-align ="left"
                label-for="url-input"
                invalid-feedback="url is required"
            >
                <b-form-input
                    id="url-input"
                    v-model="url"
                    
                    required
                ></b-form-input>
            </b-form-group>
            <b-form-group
                label="Year of Release"
                label-align ="left"
                label-for="year-input"
                invalid-feedback="year is required"
            >
                <b-form-input
                    id="year-input"
                    v-model="year"
                    
                    required
                ></b-form-input>
            </b-form-group>
            
            <b-form-group
                label="Actors"
                label-align ="left"
                label-for="actor-input"
                invalid-feedback="Actor is required"
            >
            <!-- <multiselect v-model="selectedActors" :options="getActorItems" :multiple="true" :close-on-select="false" :clear-on-select="false" :preserve-search="true" placeholder="Select Actors" label="name" track-by="name" :preselect-first="false">
    
  </multiselect> -->
                <b-form-select v-model="selectedActors" class="movie" multiple  :options="getActorItems"></b-form-select> 
                <b-button pill variant="primary" class="popupButton" @click="showModal('actors')">Add</b-button>
            </b-form-group>
            <b-form-group
                label="Producer"
                label-align ="left"
                label-for="producer-input"
                invalid-feedback="producer is required"
            >
            
                <b-form-select v-model="selectedProducer" class="movie" :options="getProducerItems"></b-form-select>
                <b-button pill variant="primary"  class="popupButton"  @click="showModal('producers')">Add</b-button>
            </b-form-group>
            <template slot="modal-footer" >
                        
                <b-button size="sm" variant="primary" @click="addMovies()">
                    Add
                </b-button>
                <b-button size="sm" @click="cancel()">
                    Cancel
                </b-button>
                
                
            </template>
        </b-modal>        

    </div>

</template>


<script>
import { mapActions, mapGetters } from "vuex";
import Multiselect from 'vue-multiselect';

export default {

    data(){
        return{
            modalShow : true,
            name : "",
            plot : "",
            year : "",
            url : "",
            actor : [],
            producer : ""

        }
    },
    components:{
        Multiselect
    },
    mounted(){
        if(this.isMovieEdit){
            this.name  = this.movieDetails.name;
            this.plot = this.movieDetails.plot;
            this.year = this.movieDetails.year;
            this.url = this.movieDetails.url;
            this.actor = this.movieDetails.actor;
            this.producer = this.movieDetails.producer;
        }
    },
    computed : {
        ...mapGetters(['actors', 'producers', 'movieDetails', 'isMovieEdit']),
        getActorItems(){
            var actors = this.actors;
            return actors.map((a) => {
                return {
                    value : a.name,
                    text : a.name
                }
            })


        },
        getProducerItems(){
            var producers = this.producers;
            return producers.map((p) =>{
                return {
                      value : p.name,
                        text : p.name
                }
            })
        }

    },
    methods :{
       ...mapActions(["showSecondModal", "addMovie", 'hideMovieModal', 'toggleEditModal']),
        showModal(category){
            this.showSecondModal(category)
        },

        addMovies(){
            this.addMovie({
                name : this.name,
                plot : this.plot,
                year : this.year,
                url : this.url,
                actor : this.actor,
                producer : this.producer
            });
            this.hideMovieModal();
        },

        cancel(){
           this.hideMovieModal();
        }
    }
    
}
</script>
