var pgp = require('pg-promise')();
var db = pgp('postgres://postgres:postgres@localhost:5433/COVIDCases');

const getConfirmedCases = (req, res, next) => {
    db.any('SELECT * FROM "covid_observations" WHERE "ObservationDate" = $1 Limit $2', [req.query.observationDate, req.query.maxCount])
    .then(data => {
        res.status(200)
            .json({
                status: 'success',
                data,
                message: 'Confirmed cases loaded'
            })
    }).catch(ex => {
        return next(ex);
    });
}

module.exports = {
    getConfirmedCases
}