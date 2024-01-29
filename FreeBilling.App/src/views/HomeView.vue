<script setup>
    import { ref, reactive, computed, onMounted } from "vue";
    import { formatMoney } from "@/formatters";
    import axios from "axios";

    const name = ref("Shawn");

    const nancy = ref("Nancy Smith");

    const bills = reactive([
        //{
        //    "hoursWorked": 15.5,
        //    "rate": 325.00,
        //    "date": "2023-05-05",
        //    "work": "I did a thing...",
        //    "customerId": 1,
        //    "employeeId": 1
        //},
        //{
        //    "hoursWorked": 20,
        //    "rate": 105.00,
        //    "date": "2023-05-06",
        //    "work": "I did another thing thing...",
        //    "customerId": 1,
        //    "employeeId": 1
        //},
        //{
        //    "hoursWorked": 15.5,
        //    "rate": 140.00,
        //    "date": "2023-05-07",
        //    "work": "I finish a thing...",
        //    "customerId": 1,
        //    "employeeId": 1
        //}
    ]);

    onMounted(async () => {
        const result = await axios("/api/customers/1/timebills");
        if (result.status === 200) {
            bills.splice(0, bills.length, ...result.data);
        }
    });

    const total = computed(() => {
        return bills.map(b => b.billingRate * b.hours)
            .reduce((b, t) => t + b, 0);
    });

    function changeMe() {
        name.value += "+";
        //alert(name);
    }

    function newItem() {
        bills.push({
           customerId: 1,
           employeeId: 1,
           hoursWorked: 5.0,
           rate: 114,
           work: "More work",
           billDate: "2023-05-08"
        });

        console.log(bills.length);
    }

</script>

<template>
    <header class="flex text-red-900">
        <h3>Our app</h3>
    </header>

    <main>
        <h1>Hello from Vue</h1>
        <div>{{ name }}</div>
        <button class="btn" @click="changeMe">Change Me</button>
        <img src="" :alt="nancy" :title="nancy" />
        <button class="btn" @click="newItem">New Item</button>
        <table>
            <thead>
                <tr>
                    <td>Hours</td>
                    <td>Date</td>
                    <td>Description</td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="b in bills">
                    <td>{{ b.hours}}</td>
                    <td>{{ b.date}} </td>
                    <td>{{ b.workPerformed}} </td>
                </tr>
            </tbody>
        </table>
        <div> Total: {{formatMoney(total)}} </div>
    </main>
</template>

