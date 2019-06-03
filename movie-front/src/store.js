import Vue from "vue";
import Vuex from "vuex";
import axios from 'axios';
import { pipeline } from "stream";

const api = axios.create({
    baseURL: "http://localhost:912"
 });

Vue.use(Vuex);


 const state = {

  category : "",
  movieDetails : {
    name : "",
    plot : "",
    url : "",
    year : "",
    actor : [],
    producer : "",
    
  },
  actorDetails : {
    name : "",
    bio : "",
    date : "",
    sex : ""
  },
  producerDetails : {
    name : "",
    bio : "",
    date : "",
    sex : ""
  },
  actors : [],
  producers  : [],
  movies :[],
  showModal : false,
  activeView : "movie",
  showFormDetails : false,
  showModalContainer : false,
  showActorModal : false,
  showProducerModal : false,
  showMore : false,
  index : 0,
  isMovieEdit : false,
  isEdit : false,
  showAlert : false,
  alertMsg : ""

 }

 const mutations = {
    changeView(state, activeView){
      state.activeView = activeView;
      state.category = activeView;
    },
    showMovieModal(state, data){
      state.showFormDetails = true;
      if(data && data.isMovieEdit){
        state.isMovieEdit = data.isMovieEdit;
        state.movieDetails = data.movieDetails;
        state.index = datta.index;
      }
      
    },
    hideMovieModal(state){
      state.showFormDetails = false;
      if(state.isMovieEdit){
        state.isMovieEdit = false;
      }
    },
    showSecondModal(state, category){
      state.showModalContainer = true;
      state.category = category;
    },
    hideModal(state){
      state.showModalContainer = false;
      state.showProducerModal = false;
      state.showActorModal = false;
      if(state.isEdit){
        state.isEdit = false;
      }
    },
    showModal(state, category){
      if(category == "actors"){
        state.showActorModal = true;
        state.showProducerModal = false;
      }else{
        state.showProducerModal = true;
        state.showActorModal = false;
      }
    }, 
    addActor(state, data){
        state.actors = state.actors.concat(data);
    },
    addProducer(state, data){
      if(state.isEdit){
        
      }else{
        state.producers = state.producers.concat(data);
      }
      
    },
    addMovie(state, data){
        state.movies = state.movies.concat(data);
    },
    editMovie(state, data){
      state.movies[state.index] = data;
      state.movies.splice();
      state.isMovieEdit = false;
    },
    editActor(state, data){
      state.actor[state.index] = data;
      state.actors.splice();
      state.isEdit = false;
    },
    editProducer(state, data){
      state.producers[state.index] = data;
      state.producers.splice();
      state.isEdit = false;
    },

    showDetails(state, data){
      state.index = data;
      state.showMore = true;
    },
    hideShowDetails(state){
      state.showMore = false;
    },
  
    deleteMovie(state, index){
      state.movies.splice(index, 1);
    },
    editActorModal(state, data){
      state.showActorModal = true;
      state.index = data.index;
      state.isEdit = data.isEdit;
      state.actorDetails = data.actorDetails;
    },
    deleteActor(state, index){
      state.actors.splice(index, 1);
    },
    editProducerModal(state, data){
      state.showProducerModal = true;
      state.index = data.index;
      state.isEdit = data.isEdit;
      state.producerDetails = data.producerDetails;
    },
    deleteProducer(state, index){
      state.producers.splice(index, 1);
    },
    getActors(state, data){
      state.actors = data;
    },
    getProducers(state,data){
      state.producers = data;
    },
    showAlertDialog(state, msg){
      state.showAlert = true;
      state.alertMsg = msg;
    }
 }

 const actions = {
      changeView(context, payload){  
        context.commit('changeView', payload)
      },

      getActors(context){
        api.get('/actor').then((response) => {
            context.commit('getActors', response.data)
        }).catch((e) => {
           context.commit('showAlertDialog', e)
        })
      },

      showMovieModal(context, payload){
        context.commit('showMovieModal', payload);
      },

      hideMovieModal(context){
        context.commit('hideMovieModal');
      },

      showSecondModal(context, payload){
        context.commit('showSecondModal', payload)
      },

      hideModal(context){
        context.commit('hideModal');
      },

      showModal(context, payload){
        context.commit('showModal', payload);
      },

      addActor(context, payload){
        api.post('/actor', payload).then((response) => {
          context.commit('addActor', response.data);
        }).catch((e) => {
          context.commit("showAlertDialog", e)
        })

      },

      editActor(context, payload){
        api.patch('/actor', payload).then((response) => {
          context.commit('editActor', response.data);
        }).catch((e) => {
          context.commit('showAlertDialog', e);
        })
      },
      editProducer(context, payload){
        api.patch('/producer', payload).then((response) => {
          context.commit('editProducer', response.data);
        }).catch((e) => {
          context.commit('showAlertDialog', e);
        })
      },
      editMovie(context, payload){
        api.patch('/movie', payload).then((response) => {
          context.commit('editMovie', response.data);
        }).catch((e) => {
          context.commit('showAlertDialog', e);
        })
      },

      addProducer(context, payload){
        api.post('/producer', payload).then((response) => {
          context.commit('addProducer', response.data);
        }).catch((e) => {
          context.commit('showAlertDialog', e);
        })
      },

      addMovie(context, payload){
        api.post('/movie', payload).then((response) => {
          context.commit('addMovie', payload);
        }).catch((e) => {
          context.commit('showAlertDialog', e);
        })
       
      },

      showDetails(context, payload){
        context.commit('showDetails', payload);
      },

      hideShowDetails(context){
        context.commit('hideShowDetails');
      },

      editMovie(context, data){
        context.commit('editMovie', data);
      },

      deleteMovie(context, index){
        api.delete(`/movie/${context.state.movies[index].id}`).then(() => {
          context.commit('deleteMovie', index);
        }).catch((e) => {
          context.commit('showAlertDialog', e);
        })
        
      },

      editActorModal(context, payload){
       
        context.commit('editActorModal', payload);
      },

      deleteActor(context, index){
        api.delete(`/actor/${context.state.actors[index].id}`).then(() => {
          context.commit('deleteActor', index);
        }).catch((e) => {
          context.commit('showAlertDialog', e);
        })
      
        
      },
      
      editProducerModal(context, payload){
        context.commit('editProducerModal', payload);
      },

      deleteProducer(context, index){
        api.delete(`/producer/${context.state.producers[index].id}`).then(() => {
          context.commit('deleteProducer', index);
        }).catch((e) => {
          context.commit('showAlertDialog', e);
        })
       
      }
 }

 const getters = {
    movies: state => {
        return state.movies;
    },
    actors: state => {
      return state.actors;
    },
    producers: state => {
      return state.producers;
    },
    category : state => {
      return state.category;
    },
    movieDetails : state => {
      return state.movieDetails;
    },
    actorDetails : state => {
      return state.actorDetails;
    },
    producerDetails : state => {
      return state.producerDetails;
    },
    showModal : state => {
      return state.showModal;
    },
    showFormDetails : state => {
      return state.showFormDetails;
    },
    activeView : state => {
      return state.activeView;
    },
    showModalContainer : state => {
      return state.showModalContainer;
    },
    showActorModal : state => {
      return state.showActorModal;
    },
    showProducerModal : state => {
      return state.showProducerModal;
    },
    index : state => {
      return state.index;
    },
    showMore : state => {
      return state.showMore;
    },
    isEdit : state => {
      return state.isEdit;
    },
    isMovieEdit : state => {
      return state.isMovieEdit;
    }, 
    showAlert : state => {
      return state.showAlert;
    },
    alertMsg : state => {
      return state.alertMsg;
    }
    
 } 


export default new Vuex.Store({
  state,
  mutations,
  actions,
  getters
});
