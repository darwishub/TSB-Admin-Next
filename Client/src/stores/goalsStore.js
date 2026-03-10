import { defineStore } from 'pinia'
import axios from 'axios'
import { ref, computed } from 'vue'

export const useGoalsStore = defineStore('goals', () => {
    const goals = ref([]);
    const goal = ref();
    let preview_goal = ref();
    let img_preview = ref();
    let category_logo = ref();

    const goalDefault = {
        "steps": [
            {
                "is_step_complete": false,
                "cards": [
                    {
                        "schema": {
                            "field_code": 0
                        },
                        "description": ""
                    }
                ],
                "step": 0
            }
        ],
        "level_code_number": "",
        "level_id": 1,
        "goal_title": "",
        "goal_subtitle": "",
        "goal_description": "",
        "goal_help_text": "",
        "goal_reward": 0,
        "goal_label": [],
        "promo_goal": false
    };
    const goalFieldSchema = [
        {
            "fields_schema": [
                {},
                {},
                {}
            ]
        }
    ];

    const fetchGoals = async (idCategory, idLevel, ShowEntries, Pagination, programId, isPublished, search, programGroupId, ALL) => {
        const { data, status } = await axios.get(`api/goals/AllGoals?categoryId=${idCategory}&codeLevel=${idLevel}&ShowEntries=${ShowEntries}&Pagination=${Pagination}&ProgramId=${programId}&isPublished=${isPublished}&Search=${search}&programGroupId=${programGroupId}&ALL=${ALL}`);
        if (status == 200) {
            goals.value = data;
        }
    };

    const DeleteGoal = async (id) => {
        const { data } = await axios.post(`api/goals/DeleteGoal?goalId=${id}`);
        goals.value = data;
    }

    const PublishGoal = async (formData) => {
        return await axios.post('api/Goals/PublishGoal', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    }

    const SaveGoal = async (formData) => {
        return await axios.post('api/Goals/SaveGoal', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    }

    const UpdateSaveGoal = async (formData) => {
        try {
            const response = await axios.post('/api/Goals/UpdateSavedGoal', formData, {
                headers: {
                    "Content-Type": "multipart/form-data"
                }
            });
            return response;
        } catch (error) {
            console.error(error);
            return null;
        }
    }

    const PublishEditGoal = async (formData) => {
        return await axios.post('/api/Goals/PublishEditGoal', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    }

    const UnpublishEditGoal = async (formData) => {
        return await axios.post('/api/Goals/UnpublishEditGoal', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    }

    const fetchEditGoal = async (id) => {
        const { data, status } = await axios.get(`/api/Goals/GetDetailsGoalById?idGoal=${id}`);
        if (status == 200) {
            goal.value = data;
        }
    };

    const addStep = async (id) => {
        return await axios.post(`/api/goals/AddStep?goalId=${id}`);
    }

    const deleteStep = async (id) => {
        return await axios.post(`/api/goals/deleteStep?stepId=${id}`);
    }

    const addField = async (id, fieldCode) => {
        return await axios.post(`/api/goals/AddField?stepId=${id}&field_code=${fieldCode}`);
    }

    const GetListQuestions = async (search, ShowEntries, Pagination, currentGoalId = 0) => {
        const { data, status } = await axios.get(`/api/goals/GetListQuestions?search=${search}&ShowEntries=${ShowEntries}&Pagination=${Pagination}&currentGoalId=${currentGoalId}`);
        if (status == 200) {
            return data;
        }
    }

    const copyQuestion = async (cardId, stepId) => {
        const { data, status } = await axios.post(`/api/goals/CopyQuestion?cardId=${cardId}&stepId=${stepId}`);
        if (status == 200) {
            return data;
        }
    }

    const ScoreCorrection = async (cardId, startupId, score) => {
        const input = {
            CardId: cardId,
            StartupId: startupId,
            Score: score
        };

        try {
            const { data } = await axios.post('/api/goals/ScoreCorrection', input);
            return data;
        } catch (error) {
            console.error(error);
            throw error;
        }
    };

    //upload file
    const uploadFile = async (formData) => {
        return await axios.post('/api/goals/CheckFileAWS', formData, {
            headers: {
                "Content-Type": "multipart/form-data",
            }
        });
    }
    // delete file
    const deleteFile = async (fileName) => {
        return await axios.post(`/api/goals/DeleteFileAWS?FileName=${fileName}`, null);
    }

    return {
        goals, goal, preview_goal, img_preview, category_logo, goalDefault, goalFieldSchema,
        fetchGoals, DeleteGoal, PublishGoal, fetchEditGoal, PublishEditGoal,
        SaveGoal, UpdateSaveGoal, addStep, deleteStep, UnpublishEditGoal, addField, GetListQuestions,
        copyQuestion, ScoreCorrection, uploadFile, deleteFile
    };
},
    {
        persist: {
            storage: localStorage,
        }
    });