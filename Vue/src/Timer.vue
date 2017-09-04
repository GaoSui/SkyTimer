<template>
  <div>
    <scrambler　ref="scrambler"></scrambler>
    <p v-bind:style="{color}">
      {{timeString}}
    </p>
  </div>
</template>

<script>
import Scrambler from './Scrambler.vue'

const States = {
  result: 0,
  prepare: 1,
  ready: 2,
  timing: 3
}

export default {
  data() {
    return {
      state: States.result,
      time: 0
    }
  },
  computed: {
    color() {
      switch (this.state) {
        case States.prepare:
          return 'red'
        case States.ready:
          return 'green'
        default:
          return 'black'
      }
    },
    timeString() {
      let mili = this.time
      let min = Math.floor(mili / 60000)
      mili %= 60000
      if (min < 10) min = '0' + min

      let sec = Math.floor(mili / 1000)
      mili %= 1000
      if (sec < 10) sec = '0' + sec

      if (mili < 10) mili = '00' + mili
      else if (mili < 100) mili = '0' + mili
      return `${min}:${sec}:${mili}`
    }
  },
  methods: {
    keyDown(e) {
      if (e.key !== ' ' || e.repeat) return
      if (this.state === States.timing) {
        this.state = States.result
        clearInterval(this.mainTick)
        this.$refs.scrambler.next()
      } else if (this.state === States.result) {
        this.state = States.prepare
        this.readyTick = setTimeout(() => {
          this.state = States.ready
        }, 800)
      }
    },
    keyUp(e) {
      if (e.key !== ' ' || e.repeat) return
      if (this.state === States.prepare) {
        this.state = States.result
        clearTimeout(this.readyTick)
      } else if (this.state === States.ready) {
        this.state = States.timing
        this.start = Date.now()
        this.mainTick = setInterval(() => this.time = Date.now() - this.start, 1)
      }
    }
  },
  created() {
    document.onkeydown = this.keyDown
    document.onkeyup = this.keyUp
  },
  components: { Scrambler }
}
</script>

<style lang="scss">
#app {

  p {
    margin: 0;
  }
}
</style>
