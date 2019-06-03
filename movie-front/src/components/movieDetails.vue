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
                    name="Name"
                    v-validate="'required'"
                    required
                ></b-form-input>
                <span>{{ errors.first('Name') }}</span>
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
                    name="Plot"
                    v-validate="'required'"
                ></b-form-input>
                <span>{{ errors.first('Plot') }}</span>
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
                    name="URL"
                    v-validate="'required|url'"
                    data-vv-as="URL"
                    required
                ></b-form-input>
                <span>{{ errors.first('URL') }}</span>
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
                    name="Year"
                    v-validate="'required|numeric|min_value:1950|max_value:2050'"
                    required
                ></b-form-input>
                <span>{{ errors.first('Year') }}</span>
            </b-form-group>
            
            <b-form-group
                label="Actors"
                label-align ="left"
                label-for="actor-input"
                invalid-feedback="Actor is required"
            >
                <b-form-select v-model="actor" class="movie multi" multiple  :options="getActorItems"></b-form-select> 
                <b-button pill variant="primary" class="popupButton" @click="showModal('actors')">Add</b-button>
                <span v-if="actorError">{{ actorError }}</span>
            </b-form-group>
            <b-form-group
                label="Producer"
                label-align ="left"
                label-for="producer-input"
                invalid-feedback="producer is required"
            >
            
                <b-form-select v-model="producer" class="movie" :options="getProducerItems">
                     <template slot="first">
                        <option :value="null" disabled>-- Please select --</option>
                    </template>
                </b-form-select>
                <b-button pill variant="primary"  class="popupButton"  @click="showModal('producers')">Add</b-button>
                <span v-if="actorError">{{ actorError }}</span>
            </b-form-group>
            <template slot="modal-footer" >
                        
                <b-button size="sm" variant="primary" @click="addMovies()">
                    {{this.isMovieEdit ? "Edit" : "Add"}}
                </b-button>
                <b-button size="sm" @click="cancel()">
                    Cancel
                </b-button>
                
                
            </template>
        </b-modal>        

    </div>

</template>


<script>
import Vue from "vue";
import VeeValidate from "vee-validate";
import { mapActions, mapGetters } from "vuex";
import Multiselect from 'vue-multiselect';

Vue.use(VeeValidate)

export default {

    data(){
        return{
            modalShow : true,
            name : "",
            plot : "",
            year : "",
            url : "",
            actor : [],
            producer : null,
            id : "",
            actorError:"",
            producerError:""
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
            this.id = this.movieDetails.id;
        }
    },
    computed : {
        ...mapGetters(['actors', 'producers', 'movieDetails', 'isMovieEdit']),
        getActorItems(){
            var actors = this.actors;
            return actors.map((a) => {
                return {
                    value : a.id,
                    text : a.name
                }
            })


        },
        getProducerItems(){
            var producers = this.producers;
            return producers.map((p) =>{
                return {
                      value : p.id,
                        text : p.name
                }
            })
        }

    },
    methods :{
       ...mapActions(["showSecondModal", "addMovie", 'hideMovieModal', 'toggleEditModal', 'editMovie']),
        showModal(category){
            this.showSecondModal(category)
        },

        addMovies(){
            this.$validator.validate().then(valid => {
                if(!valid){
                    if(this.actor.length == 0){
                        this.actorError = "Select atleast one actor."
                    }
                    if(!this.producer){
                        this.producerError = "Select a producer."
                    }
                    return;
                }
                else{
                    if(this.actor.length == 0){
                        this.actorError = "Select atleast one actor."
                    }
                    if(!this.producer){
                        this.producerError = "Select a producer."
                    }
                    if(this.actor.length == 0 || !this.producer){
                        return;
                    }
                    if(this.isMovieEdit){
                        this.editMovie({
                            name : this.name,
                            plot : this.plot,
                            year : this.year,
                            url : this.url,
                            actor : this.actor,
                            producer : this.producer,
                            id : this.id
                        })
                    }else{
                        this.addMovie({
                            name : this.name,
                            plot : this.plot,
                            year : this.year,
                            url : this.url,
                            actor : this.actor,
                            producer : this.producer
                        });
                    }
                   
                    this.hideMovieModal();
                }
            });    
        },

        cancel(){
           this.hideMovieModal();
        }
    }
    
}
</script>
