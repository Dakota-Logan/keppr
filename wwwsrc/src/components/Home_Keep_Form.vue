<template>
	<form novalidate id="keep-form" class="md-layout" @submit.prevent="newKeep">
		<label for="title">Title: </label>
		<input name="title" id="title" v-model="name" required/>
		<label for="description">Description: </label>
		<input name="description" id="description" v-model="description" required>
		<label for="img">Image Address: </label>
		<input type="text" id="img" v-model="img">
		<label for="public">Private: </label>
		<input type="checkbox" id="public" value="true" name="form" v-model="tf">
		<button type="submit">Submit</button>
	</form>
</template>

<script>
	export default {
		name: "Home_Keep_Form",
		data () {
			return {
				name: ""
				, description: ""
				, tf: false
				, img: ""
			}
		},
		methods: {
			newKeep () {
				this.$store.dispatch ("newKeep", {name: this.name, desc: this.description, tf: this.tf, img: this.img});
			}
		}
	}
</script>

<style scoped>
	@import "../assets/styles/Home_Keep_Form.css";
</style>