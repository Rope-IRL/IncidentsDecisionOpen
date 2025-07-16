import { create } from "zustand";
import {persist} from "zustand/middleware"

export const useLogin = create(persist((set, get) => ({
    isLoggedIn:false,
    id:0,
    curRole:"",

    logInEmployee: async (login, password) => {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/employeelogin/login`, 
        {
            method:"POST",
            headers:{
                "Content-Type":"application/json"
            },
            credentials:"include",
            body:JSON.stringify({
                login: login,
                hashedPassword: password
            })
        });

        if(response.ok == false){
            // set({isLoggedIn: false})
            console.log(await response.text())
        }
        else{
            const res = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/employee/info`, {
                method:"GET",
                credentials:"include"
            })
            if(res.ok)
            {
                const data = await res.json();

                set({
                    isLoggedIn:true,
                    id:data.id,
                    curRole:"employee"
                })
            }
        }
    },

    logInTechSupport: async (login, password) => {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/techsupportlogin/login`, 
        {
            method:"POST",
            headers:{
                "Content-Type":"application/json"
            },
            credentials:"include",
            body:JSON.stringify({
                login: login,
                hashedPassword: password
            })
        });

        if(response.ok == false){
            // set({isLoggedIn: false})
            console.log(await response.text())
        }
        else{
            const res = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/techsupport/info`, {
                method:"GET",
                credentials:"include"
            })
            if(res.ok)
            {
                const data = await res.json();

                set({
                    isLoggedIn:true,
                    id:data.id,
                    curRole:"techsupport"
                })
            }
        }
    }
})))

export const useIncident = create(persist((set, get) => ({
    id:0,
    day:0,
    month:0,
    year:0,
    hour:0,
    minutes:0,
    name:"", 
    description:0,
    type:"",

    setCurEditableIncident: (id, day, month, year, hour, minutes, name, description, type) =>{
        set({
            id:id,
            day:day,
            month:month,
            year:year,
            hour:hour,
            minutes:minutes,
            name:name,
            description:description,
            type:type
        })
    },

    getCurEditableIncident: () => {
        const {
            id,
            day,
            month,
            year,
            hour,
            minutes,
            name,
            description,
            type
        } = get()

        return {id, day, month, year, hour, minutes, name, description, type}
    }
})))
