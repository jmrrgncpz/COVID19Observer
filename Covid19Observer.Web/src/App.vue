<template>
  <div class="container">
    <nav class="navbar">
      <div class="navbar-brand  is-size-3 is-size-4-mobile">
        <h1 class="navbar-item">Covid19 Observer</h1>
      </div>
      <div class="navbar-menu">
        <div class="navbar-start"></div>
        <div class="navbar-end">
          <h1 class="navbar-item">
            <a href="https://jmrrgncpz.github.io/paz-portfolio/"
              >jmrrgncpz.github.io</a
            >
          </h1>
        </div>
      </div>
    </nav>
    <div class="main section">
      <div class="columns">
        <div id="parameters-container" class="column">
          <div class="box">
            <div class="field">
              <label for="" class="label">Max no. of results</label>
              <b-numberinput
                tabindex="1"
                controls-alignment="right"
                controls-position="compact"
                min="0"
                v-model="maxCountResult"
                v-on:keyup.native.enter="loadObservation"
              ></b-numberinput>
            </div>
            <div class="field">
              <label for="" class="label">Observation Date</label>
              <b-datepicker
                tabindex="2"
                placeholder="Type or select a date"
                icon="calendar"
                v-model="observationDate"
                editable
                v-on:keyup.native.enter="loadObservation"
              ></b-datepicker>
              <span class="help is-info">Format: mm/dd/yyyy</span>
            </div>
            <b-button
              class="is-primary is-fullwidth"
              v-bind:loading="isLoadingObservations"
              v-bind:disabled="isSubmitDisabled"
              v-on:click="loadObservation"
              >Submit</b-button
            >
          </div>
        </div>
        <div id="result-container" class="column is-three-quarters">
          <div
            v-show="!observationResult.length && isInitialLoad"
            class="table-placeholder"
          >
            <b-image v-bind:src="require('./assets/search.jpg')" />
            <p class="is-size-4 has-text-danger has-text-weight-bold">
              Use the controls on the left to show data.
            </p>
          </div>
          <div
            v-show="!observationResult.length && !isInitialLoad"
            class="table-placeholder"
          >
            <b-image v-bind:src="require('./assets/no_data.jpg')" />
            <p class="is-size-4 has-text-info has-text-weight-bold">
              Looks like there's nothing here.
            </p>
          </div>
          <b-tabs
            v-show="observationResult.length"
            v-model="activeTab"
            class="box"
          >
            <b-tab-item label="Table">
              <b-table
                paginated
                v-bind:data="observationResult"
                v-bind:columns="columns"
                per-page="10"
                pagination-position="top"
                striped
              >
              </b-table>
              <span class="help"
                >Showing {{ this.lastRequestParameters.maxCountResult }} rows
                for {{ this.lastRequestParameters.observationDate }}</span
              >
            </b-tab-item>
            <b-tab-item label="JSON" id="json-container">
              <pre>
                <code class="is-family-monospace">
                  {{ jsonObservations }}
                </code>
              </pre>
            </b-tab-item>
          </b-tabs>
        </div>
      </div>
    </div>

    <footer class="footer">
      <div class="level">
        <div class="level-left">
          <div class="level-item">
            <section>
              <h1 class="is-size-5">Assets attribution:</h1>
              <a href="https://www.freepik.com/vectors/data"
                >Data vector created by stories - www.freepik.com</a
              >
              <br />
              <a href="https://www.freepik.com/vectors/website"
                >Website vector created by stories - www.freepik.com</a
              >
            </section>
          </div>
        </div>
        <div class="level-right">
          <div class="level-item">
            <section class=" has-text-right">
              <a href="https://www.linkedin.com/in/jesmer-paz-24363b159/" class="social-link mr-3 is-size-3">
                <i class="fab fa-linkedin"></i>
              </a>
              <a href="https://github.com/jmrrgncpz" class="social-link mb-6 is-size-3">
                <i class="fab fa-github"></i>
              </a>
              <h1 class="is-size-7">© 2021, Jesmer Regencia Paz</h1>
            </section>
          </div>
        </div>
      </div>
      <section></section>
    </footer>
  </div>
</template>
<script>
/* eslint-disable */
import Vue from "vue";
import {
  Button,
  Datepicker,
  Numberinput,
  Field,
  Checkbox,
  Table,
  ConfigProgrammatic,
  Image,
  ToastProgrammatic as Toast,
  Tabs,
  Icon,
} from "buefy";
import "buefy/dist/buefy.css";
Vue.use(Button);
Vue.use(Datepicker);
Vue.use(Numberinput);
Vue.use(Field);
Vue.use(Checkbox);
Vue.use(Table);
Vue.use(Image);
Vue.use(Tabs);
Vue.use(Icon);
ConfigProgrammatic.setOptions({
  defaultIconPack: "fas",
});

export default Vue.extend({
  data() {
    return {
      activeTab: 0,
      columns: [
        {
          field: "country",
          label: "Country",
        },
        {
          field: "confirmed",
          label: "Confirmed",
          numeric: true,
        },
        {
          field: "deaths",
          label: "Deaths",
          numeric: true,
        },
        {
          field: "recovered",
          label: "Recovered",
          numeric: true,
        },
      ],
      isInitialLoad: true,
      isLoadingObservations: false,
      observationResult: [],
      maxCountResult: 0,
      observationDate: null,
      lastRequestParameters: {
        maxCountResult: 0,
        observationDate: null,
      },
      jsonObservations: "",
    };
  },
  methods: {
    loadObservation() {
      if (this.isSubmitDisabled) return;
      this.isLoadingObservations = true;

      const dateArray = this.getDateString(this.observationDate, "yyyy/mm/dd");
      fetch(
        `api/top/confirmed?date=${dateArray}&max_results=${this.maxCountResult}`
      )
        .then((res) => {
          switch (res.status) {
            case 200:
              return res.json();
            case 400:
              throw "Invalid request. Make sure your request parameters are correct.";
            case 500:
              throw "Something's wrong with server. Is it even on?";
            default:
          }
        })
        .then((data) => {
          this.observationResult = data.observations;

          if (this.observationResult.length) {
            Toast.open({
              message: "Data loaded successfully.",
              type: "is-success",
            });
            this.lastRequestParameters.maxCountResult = this.observationResult.length;
            this.lastRequestParameters.observationDate = this.getDateString(
              this.observationDate,
              "mm/dd/yyyy"
            );
            this.jsonObservations = JSON.stringify(
              this.observationResult,
              null,
              4
            );
          } else if (this.observationResult.length == 0) {
            Toast.open({
              message: "No data for that date :(",
              type: "is-info",
            });
          }
        })
        .catch((ex) => Toast.open({ message: ex, type: "is-danger" }))
        .finally(() => {
          this.isLoadingObservations = false;

          if (this.isInitialLoad) {
            this.isInitialLoad = false;
          }
        });
    },
    getDateString(date, format) {
      if (format == "" || format == null) {
        format = "mm/dd/yy";
      }

      const dateParts = format.split("/");
      let dateArray = [];
      dateParts.forEach((part) => {
        switch (part) {
          case "mm":
            dateArray.push(date.getMonth() + 1);
            break;
          case "yyyy":
            dateArray.push(date.getFullYear());
            break;
          case "dd":
            dateArray.push(date.getDate());
        }
      });

      return dateArray.join("/");
    },
  },
  computed: {
    isSubmitDisabled() {
      return !this.observationDate || this.maxCountResult <= 0;
    },
  },
});
</script>
<style>
@import url("./fonts.css");

body {
  font-family: Lato;
}

h1,
h2,
h3,
h4,
h5,
h6 {
  font-family: Montserrat-Bold;
}

.table-placeholder {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.table-placeholder > .image {
  width: 300px;
}

#json-container > pre {
  max-height: 500px;
}
</style>
