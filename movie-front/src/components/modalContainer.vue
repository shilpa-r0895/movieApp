<template>
    <div>
       
        <b-modal  v-model="modalShow"  @hidden="resetModal" centered :title="this.title">
                    <b-form-group
                    label="Name"
                    label-for="name-input"
                    invalid-feedback="Name is required"
                    >
                        <b-form-input
                            id="name-input"
                            v-model="name"
                            
                            required
                        ></b-form-input>
                    </b-form-group>
                    <b-form-group label="Date of Birth">
                        <datepicker placeholder="Select Date" v-model="date"></datepicker>
                     </b-form-group>
                     <b-form-group
                    label="Bio"
                    label-for="bio-input"
                    invalid-feedback="Bio is required"
                    >
                        <b-form-input
                            id="bio-input"
                            v-model="bio"
                            
                            required
                        ></b-form-input>
                    </b-form-group>
                    
                    <b-form-group label="Sex">
                        <b-form-radio v-model="sex" name="some-radios" value="male">Male</b-form-radio>
                        <b-form-radio v-model="sex" name="some-radios" value="female">Female</b-form-radio>
                    </b-form-group>
                    

                

                <template slot="modal-footer" >
                    
                    <b-button size="sm" variant="primary" @click="ok()">
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
import Vue from "vue"
import Datepicker from "vuejs-datepicker/dist/vuejs-datepicker.esm.js";
import { mapActions, mapGetters } from "vuex";

export default {
     name : "ModalContainer",
     //props: ['isEdit'],
     data() {
      return {
        modalShow: true,
        name:"",
        bio: "",
        date: "",
        sex: "",
        id : ""
      }
    },
    components:{
        Datepicker
    },
    mounted(){
        if(this.isEdit){
            
             if(this.category == 'actors'){
               this.name = this.actorDetails.name;
               this.bio = this.actorDetails.bio;
               this.date = this.actorDetails.date;
               this.sex = this.actorDetails.sex;
               this.id = this.actorDetails.id;
            }else{
                this.name = this.producerDetails.name;
                this.bio = this.producerDetails.bio;
                this.date = this.producerDetails.date;
                this.sex = this.producerDetails.sex;
                this.id = this.producerDetails.id;
            }
        }
    },
    computed : {
       ...mapGetters(['category', 'actorDetails', 'producerDetails', 'isEdit']),
       title() {
           return this.getTitle();
       }
       
    },
    methods : {
        ...mapActions(['hideModal', 'addProducer', 'addActor', 'editActor', 'editProducer']),
        getTitle(){
            var cat = this.category;
            if(cat == "actors"){
                return "Enter Actor Details";
            }else{
                return "Enter Producer Details";
            }
        },
        ok(){
            if(this.category == 'actors'){
                if(this.isEdit){
                    this.editActor({
                        name : this.name,
                        date : this.date,
                        bio : this.bio,
                        sex : this.sex,
                        id : this.id
                    })
                }else{
                     this.addActor({
                        name : this.name,
                        date : this.date,
                        bio : this.bio,
                        sex : this.sex
                    })
                }
               
            }else{
                 if(this.isEdit){
                    this.editProducer({
                        name : this.name,
                        date : this.date,
                        bio : this.bio,
                        sex : this.sex,
                        id : this.id
                    })
                }else{
                    this.addProducer({
                        name : this.name,
                        date : this.date,
                        bio : this.bio,
                        sex : this.sex
                    })
                }
            }
            this.hideModal();

        },
        resetModal(){
           this.hideModal()
        },
        cancel(){
            this.hideModal();
        }
       
    }
}
</script>
