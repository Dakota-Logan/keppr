<template>
	<form novalidate id="keep-form" class="md-layout" @submit.prevent="newKeep">
		<label for="title">Title: </label>
		<input name="title" id="title" v-model="name" required/>
		<label for="description">Description: </label>
		<input name="description" id="description" v-model="description" required>
		<button type="submit">Submit</button>
	</form>
</template>

<script>
	export default {
		name: "Home_Keep_Form",
		data () {
			return {
				name: "",
				description: ""
			}
		},
		methods: {
			newKeep () {
				this.$store.dispatch("newVault", {name: this.name, desc: this.description})
			}
		}
	}
</script>

<style scoped>
	@import "../assets/styles/Home_Keep_Form.css";
</style>