<template>
    <div>
        <div v-if="actors.length">
            <b-list-group >
                <b-list-group-item v-for="(actor, index) in actors" :key="index" class="flex-column align-items-start  primary list-items">
                    <div class="d-flex w-100 justify-content-between">
                        <h5 class="mb-1 name">{{actor.name}}</h5>
                    </div>
                    <div class="align-left  w-50">
                         <span class="bold">Bio : </span>  
                        <p class="mb-1">
                        {{actor.bio}}
                        </p>
                    </div>

                    <div class="align-left w-50">
                        <span class="bold"> Date of Birth : </span>
                        <span>{{getDate(actor.date)}}</span>
                    </div>
                    <div class="align-left w-50">
                        <span class="bold">Sex : </span>
                        <span>{{actor.sex}}</span>
                    </div>
                    <div class="icons">
                        <b-button variant="link" class="icon-button" pills @click="editActorDetails(index)"><v-icon name="edit"></v-icon></b-button>
                        <b-button variant="link" class="icon-button" pills @click="deleteActors(index)"><v-icon name="trash"></v-icon></b-button>
                    </div>
                </b-list-group-item>
            </b-list-group>
        </div>
        <div v-else>
            No Actors Added
        </div>
    </div>
</template>

<script>

import {mapGetters, mapActions} from 'vuex';

export default {

    computed:{
        ...mapGetters(["actors"])
    },
    methods : {
        ...mapActions(["editActorModal", "deleteActor"]),
        getDate(dob){
            var dt = new Date(dob);
            var d = dt.getDate() < 10 ? "0" + dt.getDate() : dt.getDate();
            var m = dt.getMonth() < 10 ? '0' + dt.getMonth() : dt.getMonth();
            var finalDOB = d + '/' + m + '/' + dt.getFullYear();
            return finalDOB;
        },
        editActorDetails(index){
            this.editActorModal({
                isEdit : true,
                actorDetails : this.actors[index],
                index : index
            });
        },
        deleteActors(index){
            this.deleteActor(index);
        }
    }
    
}
</script>
