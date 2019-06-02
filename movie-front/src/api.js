import axios from 'axios';

const api = axios.create({
    baseURL: "http://localhost:912"
 });
export const apiService = {
     getActors = () => {
        return await api.get("/actor");
      },
      
       addActor = (actor) => {
        return await api.post(`/actor`,actor);
      },
      
       editActor = (actor) => {
        return await api.patch(`/actor`,actor);
      },
      
       deleteActor = (actorId) => {
         return await api.delete(`/actor`, {actorId});
      }
}