var pgp = require('pg-promise')();
var db = pgp('postgres://postgres:postgres@localhost:5433/COVIDCases');

const getConfirmedCases = (req, res, next) => {
    db.any(`
        SELECT "Country" as country,
            "Confirmed" as confirmed,
            "Deaths" as deaths,
            "Recovered" as recovered
        FROM "covid_observations"
        WHERE "ObservationDate" = $1
        ORDER BY "confirmed" DESC
        LIMIT $2`,
    [req.query.date, parseInt(req.query.max_results)])
    .then(data => {
        res.status(200)
            .json({ observations: data })
    }).catch(ex => {
        return next(ex);
    });
}

module.exports = {
    getConfirmedCases
}