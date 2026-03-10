import { defineStore } from 'pinia'
import axios from 'axios'

export const useGoalCategoriesStore = defineStore({
    'id': 'goal_category',
    state: () => ({
        goal_categories: [],
        goal_category: null,
        goal_index: 0
    }),
    getters: {
        getGoalCategoriesById: (state) => {
            return (categoryId) => state.goal_categories.find(x => x.id == categoryId);
        }
    },
    actions: {
        async fetchGoalCategories() {
            await axios.get("https://api.npoint.io/e666d836ba3c05b64580").then(res => {
                this.goal_categories = res.data.goal_categories;
            });
        },
        fetchGoalCategory(id){
            this.goal_category = this.goal_categories.find(x => x.id == id);
            this.goal_index = this.goal_categories.findIndex(i => i.id === this.goal_category.id);
        },
        nextCategoryDetail(){
            if(this.goal_index === this.goal_categories.length - 1){
                this.goal_index = 0;
                this.goal_category = this.goal_categories[this.goal_index];
            } else {
                this.goal_category = this.goal_categories[this.goal_index + 1];
                this.goal_index = this.goal_index + 1;
            }
        },
        prevCategoryDetail(){
            if(this.goal_index === 0){
                this.goal_index = this.goal_categories.length - 1;
                this.goal_category = this.goal_categories[this.goal_index];
            } else {
                this.goal_category = this.goal_categories[this.goal_index - 1];
                this.goal_index = this.goal_index - 1;
            }
        }
    }
});