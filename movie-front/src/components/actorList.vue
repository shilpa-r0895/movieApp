<template>
    <div>
        <div v-if="actors.length">
            <b-list-group >
                <b-list-group-item v-for="(actor, index) in actors" :key="index" class="flex-column align-items-start">
                    <div class="d-flex w-100 justify-content-between">
                        <h5 class="mb-1">{{actor.name}}</h5>
                    </div>

                    <p class="mb-1">
                    {{actor.bio}}
                    </p>

                    <div>
                        <span>{{getDate(actor.date)}}</span>
                    </div>
                    <div>
                        <span>{{actor.sex}}</span>
                    </div>
                    <div class="icons">
                        <b-button variant="primary" class="icon-button" pills @click="editActorDetails(index)"><v-icon name="edit"></v-icon></b-button>
                        <b-button variant="primary" class="icon-button" pills @click="deleteActors(index)"><v-icon name="trash"></v-icon></b-button>
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
            var d = dob.getDate() < 10 ? "0" + dob.getDate() : dob.getDate();
            var m = dob.getMonth() < 10 ? '0' + dob.getMonth() : dob.getMonth();
            var finalDOB = d + '/' + m + '/' + dob.getFullYear();
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
